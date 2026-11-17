Shader "Custom/TextureMoveEffect" {
	Properties {
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Source Blend Factor", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend Factor", Float) = 10
		_ElementColor ("Element Color Tint", Vector) = (1,1,1,1)
		_DarkenAmount ("Darken Amount", Range(0, 1)) = 0
		_Cutoff ("Alpha Smooth Threshold", Range(0, 1)) = 0
		_ElementTex ("Element Texture", 2D) = "white" {}
		_ElementTex2 ("Element Texture 2", 2D) = "white" {}
		_ElementMoveDirection ("Element Move Direction", Vector) = (0.1,-0.1,0,0)
		_ElementSpeed ("Movement Speed", Range(0, 10)) = 1
		_ElementOpacity ("Element Opacity", Range(0, 1)) = 1
		_ElementToggle ("Element Visibility Toggle", Range(0, 1)) = 1
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
	Fallback "Hidden/Universal Render Pipeline/FallbackError"
}