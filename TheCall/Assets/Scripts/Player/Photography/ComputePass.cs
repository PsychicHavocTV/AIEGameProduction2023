using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

class ComputePass : CustomPass
{
    [Tooltip("The Key Object layer.")]
    public LayerMask objLayer = 0;

    protected ShaderTagId[] shaderTags; //

    private RTHandle maskBuffer; // The buffer for the mask render target.

    private Material whiteMaterial; // Pure white material.

    [SerializeField, HideInInspector] // Workaround so the shader is included in the final build.
    private Shader whiteShader; // Pure white shader.

    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        if (whiteShader == null) // Get a reference to the shader.
            whiteShader = Shader.Find("Hidden/Custom/White");

        whiteMaterial = CoreUtils.CreateEngineMaterial(whiteShader); // Create the white material.

        // Setup shader tags.
        shaderTags = new ShaderTagId[4]
        {
            new ShaderTagId("Forward"),
            new ShaderTagId("ForwardOnly"),
            new ShaderTagId("SRPDefaultUnlit"),
            new ShaderTagId("FirstPass"),
        };
    }

    /// <summary>
    /// Allocate buffers for mask render target.
    /// </summary>
    void AllocateMask()
    {
        if (maskBuffer?.rt == null || !maskBuffer.rt.IsCreated()) // Allocate mask buffer if it doesn't exist.
        {
            maskBuffer = RTHandles.Alloc(
                    Vector2.one, TextureXR.slices, dimension: TextureXR.dimension,
                    colorFormat: GraphicsFormat.R8_UNorm,
                    useDynamicScale: true, name: "_MaskBuffer"
                );
        }
    }

    protected override void Execute(CustomPassContext ctx)
    {
        AllocateMask(); // Allocate buffers for mask render target. 

        if (whiteMaterial == null) // Return if white material doesn't exist.
            return;

        // Draw renderers within Key Object layer to the mask's render target.
        CoreUtils.SetRenderTarget(ctx.cmd, maskBuffer, ctx.cameraDepthBuffer, ClearFlag.Color); // Only clear color so that the objects get culled by the camera's depth buffer.
        CustomPassUtils.DrawRenderers(ctx, objLayer, 
            overrideMaterial: whiteMaterial, overrideMaterialIndex: whiteMaterial.FindPass("FirstPass"),
            overrideRenderState: new RenderStateBlock(RenderStateMask.Depth)
            {
                depthState = new DepthState(true, CompareFunction.LessEqual),
            });

        // Set global texture.
        ctx.cmd.SetGlobalTexture("_MaskBuffer", maskBuffer);

    }

    protected override void Cleanup()
    {
        CoreUtils.Destroy(whiteMaterial); // Destroy white material.
        maskBuffer?.Release(); // Release mask buffer.
    }
}