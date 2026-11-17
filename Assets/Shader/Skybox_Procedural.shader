Shader "Skybox/Procedural" {
	Properties {
		_SunTint ("Sun Tint", Vector) = (1,1,1,1)
		_SunStrength ("Sun Strength", Float) = 1
		_Color ("Atmosphere Tint", Vector) = (0.5,0.5,0.5,1)
		_GroundColor ("Ground", Vector) = (0.369,0.349,0.341,1)
		_HdrExposure ("HDR Exposure", Float) = 1.3
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 _Color;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return _Color; // RGBA
			}

			ENDHLSL
		}
	}
}