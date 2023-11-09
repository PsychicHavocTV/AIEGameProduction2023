using UnityEngine;
using UnityEngine.Rendering;

// https://www.youtube.com/watch?v=YP723zBCXfk

[RequireComponent(typeof(Camera))]
public class AreaCompute : MonoBehaviour
{
    [Tooltip("Reference to the area compute shader.")]
    public ComputeShader compute_shader;
    [HideInInspector] public uint[] data; // The data obtained by the compute buffer.

    protected ComputeBuffer compute_buffer; // Compute buffer to obtain data from the compute shader.

    /// <summary>
    /// The final calculated value for how much area the object takes up on the screen. (0-1 range)
    /// </summary>
    public float Area
    {
        get => m_area;
    }
    private float m_area = 0.0f;

    private int m_width, m_height; // The width and height of the mask buffer.

    private int m_handle_init; // The handle for the compute shader's init kernel.
    private int m_handle_main; // The handle for the compute shader's main kernel.

    void OnEnable()
    {
        m_handle_init = compute_shader.FindKernel("CSInit"); // Grab The handle for the compute shader's init kernel.
        m_handle_main = compute_shader.FindKernel("CSMain"); // Grab The handle for the compute shader's main kernel.

        // Create the compute buffer and initialize the data.
        compute_buffer = new ComputeBuffer(1, sizeof(uint));
        data = new uint[1];

        RenderPipelineManager.endCameraRendering += ComputeArea; // Call the ComputeArea method when the camera finishes rendering.
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= ComputeArea; // Don't call the ComputeArea method when the camera finishes rendering.
        Cleanup();
    }

    void OnGUI()
    {
        // [DEBUG]
        // Just draws text to say what percentage of area the object takes up on the screen.
        GUI.Label(new Rect(10, 10, 300, 20), (m_area * 100.0f).ToString());
    }

    /// <summary>
    /// Executes the compute shader and obtains the data from it, then calculates the final area percentage (0-1 range).
    /// </summary>
    public void ComputeArea(ScriptableRenderContext context, Camera camera)
    {
        m_width = Screen.width; // Find width and height of the mask buffer.
        m_height = Screen.height;

        // Setup compute shader and pass the mask buffer from global texture.
        compute_shader.SetTextureFromGlobal(m_handle_main, "image", "_MaskBuffer");
        compute_shader.SetBuffer(m_handle_main, "compute_buffer", compute_buffer);
        compute_shader.SetBuffer(m_handle_init, "compute_buffer", compute_buffer);

        // Execute each kernel within the compute shader.
        compute_shader.Dispatch(m_handle_init, 64, 1, 1);
        compute_shader.Dispatch(m_handle_main, m_width / 8, m_height / 8, 1);

        // Obtain data from the compute shader.
        compute_buffer.GetData(data);
        uint result = data[0];

        // Calculate the final area percentage.
        m_area = (float)result / ((float)m_width * (float)m_height);
    }

    void Cleanup()
    {
        // Release compute buffer.
        if (compute_buffer != null)
        {
            compute_buffer.Release();
            compute_buffer = null;
        }
    }
}
