Shader "Hidden/Universal/CoreBlit"
{
    Properties
    {
        _BlitTexture ("Texture", 2D) = "white" {}
        _BlitScaleBias ("Scale Bias", Vector) = (1, 1, 0, 0)
        _BlitTexture_TexelSize ("Texel Size", Vector) = (0, 0, 0, 0)
        _BlitMipLevel ("Mip Level", Float) = 0
    }

    HLSLINCLUDE
        #pragma target 2.0

        Texture2D _BlitTexture;
        SamplerState sampler_LinearClamp;
        SamplerState sampler_PointClamp;
        float4 _BlitScaleBias;
        float4 _BlitTexture_TexelSize;
        float _BlitMipLevel;

        struct Attributes
        {
            uint vertexID : SV_VertexID;
        };

        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float2 texcoord : TEXCOORD0;
        };

        // Matches Unity's GetFullScreenTriangleVertexPosition / GetFullScreenTriangleTexCoord
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

        // Matches Unity's GetQuadVertexPosition / GetQuadTexCoord (2 triangles, indices 0,1,2,3)
        Varyings VertQuad(Attributes input)
        {
            Varyings output;
            uint topBit = input.vertexID >> 1;
            uint botBit = input.vertexID & 1;
            float x = topBit;
            float y = 1 - ((topBit + botBit) & 1);
            output.positionCS = float4(x * 2.0 - 1.0, y * 2.0 - 1.0, UNITY_NEAR_CLIP_VALUE, 1.0);
            float u = topBit;
            float v = (topBit + botBit) & 1;
            #if UNITY_UV_STARTS_AT_TOP
                v = 1.0 - v;
            #endif
            output.texcoord = float2(u, v) * _BlitScaleBias.xy + _BlitScaleBias.zw;
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

        Pass
        {
            Name "NearestQuad"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragNearest
            ENDHLSL
        }

        Pass
        {
            Name "BilinearQuad"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragBilinear
            ENDHLSL
        }

        Pass
        {
            Name "NearestQuadPadding"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragNearest
            ENDHLSL
        }

        Pass
        {
            Name "BilinearQuadPadding"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragBilinear
            ENDHLSL
        }

        Pass
        {
            Name "NearestQuadPaddingRepeat"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragNearest
            ENDHLSL
        }

        Pass
        {
            Name "BilinearQuadPaddingRepeat"
            ZWrite Off ZTest Always Blend Off Cull Off
            HLSLPROGRAM
                #pragma vertex VertQuad
                #pragma fragment FragBilinear
            ENDHLSL
        }
    }

    Fallback Off
}
