Shader "Custom/MobileWater" {
	Properties {
		_BaseColor ("Water Color", Vector) = (0,0.4,0.7,0.8)
		_NormalMap ("Normal Map", 2D) = "bump" {}
		_HeightMap ("Height Map", 2D) = "white" {}
		[Header(Movement Layer A)] _DirA ("Direction A (X, Y)", Vector) = (0.1,0,0,0)
		_SpeedA ("Speed A", Range(0, 5)) = 1
		[Header(Movement Layer B)] _DirB ("Direction B (X, Y)", Vector) = (0,0.1,0,0)
		_SpeedB ("Speed B", Range(0, 5)) = 1.2
		[Header(Settings)] _HeightStrength ("Wave Height", Range(0, 1)) = 0.2
		_Tiling ("Texture Tiling", Float) = 1
		_GlintColor ("Reflection Glint", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
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

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Transparent/VertexLit"
}