using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

public class ComputePass : CustomPass
{
    [Tooltip("The Key Object layer.")]
    public LayerMask objectLayer = 0;

    [Tooltip("Reference to the area compute shader.")]
    public ComputeShader computeShader;
    [HideInInspector] public uint[] data; // The data obtained by the compute buffer.

    [Tooltip("Reference to the objects in view script, to pass the final calculation to.")]
    public ObjectsInView viewObjectsScript;

    protected ComputeBuffer computeBuffer; // Compute buffer to obtain data from the compute shader.

    protected ShaderTagId[] shaderTags; //

    private RTHandle maskBuffer; // The buffer for the mask render target.

    private int m_handle_init; // The handle for the compute shader's init kernel.
    private int m_handle_main; // The handle for the compute shader's main kernel.

    private int m_bufferWidth, m_bufferHeight; // The width and height of the mask buffer.

    private Material whiteMaterial; // Pure white material.

    [SerializeField, HideInInspector] // Workaround so the shader is included in the final build.
    Shader whiteShader; // Pure white shader.

    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        if (whiteShader == null) // Get a reference to the shader.
        {
            whiteShader = Shader.Find("Hidden/Custom/White"); // Find by shader path.
            if (whiteShader == null) // If still not found.
            {
                whiteShader = Resources.Load<Shader>("Shaders/White"); // Find by resource path.
            }
        }

        if (whiteShader == null) // Can't find shader reference.
            return;

        whiteMaterial = CoreUtils.CreateEngineMaterial(whiteShader); // Create the white material.

        // Setup shader tags.
        shaderTags = new ShaderTagId[4]
        {
            new ShaderTagId("Forward"),
            new ShaderTagId("ForwardOnly"),
            new ShaderTagId("SRPDefaultUnlit"),
            new ShaderTagId("FirstPass"),
        };

        m_handle_init = computeShader.FindKernel("CSInit"); // Grab The handle for the compute shader's init kernel.
        m_handle_main = computeShader.FindKernel("CSMain"); // Grab The handle for the compute shader's main kernel.

        // Create the compute buffer and initialize the data.
        computeBuffer = new ComputeBuffer(1, sizeof(uint));
        data = new uint[1];
    }

    /// <summary>
    /// Allocate buffers for mask render target.
    /// </summary>
    private void AllocateMask()
    {
        if (maskBuffer?.rt == null || !maskBuffer.rt.IsCreated()) // Allocate mask buffer if it doesn't exist.
        {
            maskBuffer = RTHandles.Alloc(
                    Vector2.one, TextureXR.slices, dimension: TextureXR.dimension,
                    colorFormat: GraphicsFormat.R8_UNorm,
                    useDynamicScale: true, name: "_MaskBuffer"
                );

            m_bufferWidth = maskBuffer.rt.width; // Get width of buffer.
            m_bufferHeight = maskBuffer.rt.height;  // Get height of buffer.
        }
    }

    protected override void Execute(CustomPassContext ctx)
    {
        if (whiteMaterial == null) // Return if white material doesn't exist.
            return;

        AllocateMask(); // Allocate buffers for mask render target. 

        // Draw renderers within Key Object layer to the mask's render target.
        CoreUtils.SetRenderTarget(ctx.cmd, maskBuffer, ctx.cameraDepthBuffer, ClearFlag.Color); // Only clear color so that the objects get culled by the camera's depth buffer.
        CustomPassUtils.DrawRenderers(ctx, objectLayer, overrideMaterial: whiteMaterial);

        // Setup compute shader and pass the mask buffer by texture.
        computeShader.SetTexture(m_handle_main, "image", maskBuffer);
        computeShader.SetBuffer(m_handle_main, "compute_buffer", computeBuffer);
        computeShader.SetBuffer(m_handle_init, "compute_buffer", computeBuffer);

        // Execute each kernel within the compute shader.
        ctx.cmd.DispatchCompute(computeShader, m_handle_init, 64, 1, 1);
        ctx.cmd.DispatchCompute(computeShader, m_handle_main, m_bufferWidth / 8, m_bufferHeight / 8, 1);

        // Obtain data from the compute shader.
        computeBuffer.GetData(data);
        uint result = data[0];

        // Calculate the final area percentage.
        viewObjectsScript.Area = (float)result / ((float)m_bufferWidth * (float)m_bufferHeight);

    }

    protected override void Cleanup()
    {
        // Release compute buffer.
        if (computeBuffer != null)
        {
            computeBuffer.Release();
            computeBuffer = null;
        }

        // Cleanup pass.
        if (whiteMaterial != null)
            CoreUtils.Destroy(whiteMaterial); // Destroy white material.
        maskBuffer?.Release(); // Release mask buffer.
    }
}