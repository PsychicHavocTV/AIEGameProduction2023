using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

class ComputePass : CustomPass
{
    public Material whiteMat;
    public LayerMask objMask = 0;

    ShaderTagId[] shaderTags;

    RTHandle maskBuffer;
    RTHandle maskDepthBuffer;

    protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
    {
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
        if (maskDepthBuffer?.rt == null || !maskDepthBuffer.rt.IsCreated())
        {
            maskDepthBuffer = RTHandles.Alloc(
                    Vector2.one, TextureXR.slices, dimension: TextureXR.dimension,
                    colorFormat: GraphicsFormat.R16_UInt,
                    useDynamicScale: true, name: "_MaskBufferDepth", depthBufferBits: DepthBits.Depth16
                );
        }
    }

    protected override void Execute(CustomPassContext ctx)
    {
        AllocateMask();

        if (whiteMat == null)
            return;

        CoreUtils.SetRenderTarget(ctx.cmd, maskBuffer, maskDepthBuffer, ClearFlag.All);
        CustomPassUtils.DrawRenderers(ctx, objMask, overrideMaterial: whiteMat, overrideRenderState: new RenderStateBlock(RenderStateMask.Depth)
        {
            depthState = new DepthState(true, CompareFunction.LessEqual)
        });

        ctx.cmd.SetGlobalTexture("_CountMap", maskBuffer);

    }

    protected override void Cleanup()
    {
        maskDepthBuffer?.Release();
        maskBuffer?.Release();
    }
}