using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using RendererListDesc = UnityEngine.Rendering.RendererUtils.RendererListDesc;

class ComputePass : CustomPass
{
    public LayerMask objLayer = 0;

    Material whiteMaterial;

    ShaderTagId[] shaderTags;

    RTHandle maskBuffer;

    [SerializeField, HideInInspector]
    Shader whiteShader;

    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
        if (whiteShader == null)
            whiteShader = Shader.Find("Hidden/Custom/White");

        whiteMaterial = CoreUtils.CreateEngineMaterial(whiteShader);

        shaderTags = new ShaderTagId[4]
        {
            new ShaderTagId("Forward"),
            new ShaderTagId("ForwardOnly"),
            new ShaderTagId("SRPDefaultUnlit"),
            new ShaderTagId("FirstPass"),
        };
    }

    void AllocateMask()
    {
        if (maskBuffer?.rt == null || !maskBuffer.rt.IsCreated())
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
        AllocateMask();

        if (whiteMaterial == null)
            return;

        CoreUtils.SetRenderTarget(ctx.cmd, maskBuffer, ctx.cameraDepthBuffer, ClearFlag.Color);
        CustomPassUtils.DrawRenderers(ctx, objLayer, 
            overrideMaterial: whiteMaterial, overrideMaterialIndex: whiteMaterial.FindPass("FirstPass"),
            overrideRenderState: new RenderStateBlock(RenderStateMask.Depth)
            {
                depthState = new DepthState(true, CompareFunction.LessEqual),
            });

        ctx.cmd.SetGlobalTexture("_MaskBuffer", maskBuffer);

    }

    protected override void Cleanup()
    {
        CoreUtils.Destroy(whiteMaterial);
        maskBuffer?.Release();
    }
}