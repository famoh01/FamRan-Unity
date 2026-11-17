Shader "Hidden/Universal/CoreBlitColorAndDepth"
{
    Properties
    {
        _BlitTexture ("Texture", 2D) = "white" {}
        _BlitScaleBias ("Scale Bias", Vector) = (1, 1, 0, 0)
    }

    HLSLINCLUDE
        #pragma target 2.0

        Texture2D _BlitTexture;
        SamplerState sampler_LinearClamp;
        SamplerState sampler_PointClamp;
        float4 _BlitScaleBias;

        struct Attributes
        {
            uint vertexID : SV_VertexID;
        };

        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float2 texcoord : TEXCOORD0;
        };

        Varyings Vert(Attributes input)
        {
            Varyings output;
            float2 uv = float2((input.vertexID << 1) & 2, input.vertexID & 2);
            output.positionCS = float4(uv * 2.0 - 1.0, UNITY_NEAR_CLIP_VALUE, 1.0);
            #if UNITY_UV_STARTS_AT_TOP
                output.texcoord = float2((input.vertexID << 1) & 2, 1.0 - (input.vertexID & 2));
            #else
                output.texcoord = float2((input.vertexID << 1) & 2, input.vertexID & 2);
            #endif
            output.texcoord = output.texcoord * _BlitScaleBias.xy + _BlitScaleBias.zw;
            return output;
        }

        half4 FragNearest(Varyings input) : SV_Target
        {
            return _BlitTexture.Sample(sampler_PointClamp, input.texcoord);
        }

        half4 FragBilinear(Varyings input) : SV_Target
        {
            return _BlitTexture.Sample(sampler_LinearClamp, input.texcoord);
        }
    ENDHLSL

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Nearest"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex Vert
                #pragma fragment FragNearest
            ENDHLSL
        }

        Pass
        {
            Name "Bilinear"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex Vert
                #pragma fragment FragBilinear
            ENDHLSL
        }
    }

    Fallback Off
}
