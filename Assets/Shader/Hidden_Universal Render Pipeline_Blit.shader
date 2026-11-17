Shader "Hidden/Universal Render Pipeline/Blit"
{
    Properties
    {
        _BlitTexture ("Texture", 2D) = "white" {}
        _BlitScaleBias ("Scale Bias", Vector) = (1, 1, 0, 0)
        _BlitColor ("Blit Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        LOD 100

        Pass
        {
            Name "Blit"

            ZTest Always
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0

            Texture2D _BlitTexture;
            SamplerState sampler_BlitTexture;

            float4 _BlitScaleBias;
            half4 _BlitColor;

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
                output.positionCS = float4(uv * 2.0 - 1.0, 0.0, 1.0);
                #if UNITY_UV_STARTS_AT_TOP
                    output.texcoord = float2(uv.x, 2.0 - uv.y);
                #else
                    output.texcoord = uv;
                #endif
                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord * _BlitScaleBias.xy + _BlitScaleBias.zw;
                half4 color = _BlitTexture.Sample(sampler_BlitTexture, uv);
                return color * _BlitColor;
            }
            ENDHLSL
        }
    }

    Fallback Off
}
