Shader "Hidden/Custom/White"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _ColorMap("ColorMap", 2D) = "white" {}

        _AlphaCutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
    }

    HLSLINCLUDE

    #pragma target 4.5
    #pragma only_renderers d3d11 ps5 xboxone vulkan metal switch

    #pragma multi_compile_instancing

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "FirstPass"
            Tags { "LightMode" = "FirstPass" }

            Blend Off
            ZWrite Off
            ZTest LEqual

            Cull Back

            HLSLPROGRAM

            #define _ALPHATEST_ON

            #define _ENABLE_FOG_ON_TRANSPARENT

            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT

            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TANGENT_TO_WORLD

            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassRenderers.hlsl"

            TEXTURE2D(_ColorMap);
            float4 _ColorMap_ST;
            float4 _Color;

            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 viewDirection, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
                float2 colorMapUv = TRANSFORM_TEX(fragInputs.texCoord0.xy, _ColorMap);
                float4 result = SAMPLE_TEXTURE2D(_ColorMap, s_trilinear_clamp_sampler, colorMapUv) * _Color;
                float opacity = result.a;
                float3 color = result.rgb;

                #ifdef _ALPHATEST_ON
                DoAlphaTest(opacity, _AlphaCutoff);
                #endif

                ZERO_INITIALIZE(BuiltinData, builtinData);
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                builtinData.opacity = opacity;
                builtinData.emissiveColor = float3(0, 0, 0);
                surfaceData.color = float3(1, 1, 1);
            }

            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassForwardUnlit.hlsl"

            #pragma vertex Vert
            #pragma fragment Frag

            ENDHLSL
        }
    }
}
