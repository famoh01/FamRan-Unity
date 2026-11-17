Shader "Custom/CombinedSlotsMain" {
	Properties {
		_SlotCount ("Slot Count", Float) = 1
		_Tex0 ("Slot 0", 2D) = "white" {}
		_ST0 ("Slot 0 ST (scale.xy, offset.zw)", Vector) = (1,1,0,0)
		_Color0 ("Slot 0 Color", Vector) = (1,1,1,1)
		_Cut0 ("Slot 0 Cutoff", Range(0, 1)) = 0.5
		_Tex1 ("Slot 1", 2D) = "white" {}
		_ST1 ("Slot 1 ST", Vector) = (1,1,0,0)
		_Color1 ("Slot 1 Color", Vector) = (1,1,1,1)
		_Cut1 ("Slot 1 Cutoff", Range(0, 1)) = 0.5
		_Tex2 ("Slot 2", 2D) = "white" {}
		_ST2 ("Slot 2 ST", Vector) = (1,1,0,0)
		_Color2 ("Slot 2 Color", Vector) = (1,1,1,1)
		_Cut2 ("Slot 2 Cutoff", Range(0, 1)) = 0.5
		_Tex3 ("Slot 3", 2D) = "white" {}
		_ST3 ("Slot 3 ST", Vector) = (1,1,0,0)
		_Color3 ("Slot 3 Color", Vector) = (1,1,1,1)
		_Cut3 ("Slot 3 Cutoff", Range(0, 1)) = 0.5
		_Tex4 ("Slot 4", 2D) = "white" {}
		_ST4 ("Slot 4 ST", Vector) = (1,1,0,0)
		_Color4 ("Slot 4 Color", Vector) = (1,1,1,1)
		_Cut4 ("Slot 4 Cutoff", Range(0, 1)) = 0.5
		_Tex5 ("Slot 5", 2D) = "white" {}
		_ST5 ("Slot 5 ST", Vector) = (1,1,0,0)
		_Color5 ("Slot 5 Color", Vector) = (1,1,1,1)
		_Cut5 ("Slot 5 Cutoff", Range(0, 1)) = 0.5
		_Tex6 ("Slot 6", 2D) = "white" {}
		_ST6 ("Slot 6 ST", Vector) = (1,1,0,0)
		_Color6 ("Slot 6 Color", Vector) = (1,1,1,1)
		_Cut6 ("Slot 6 Cutoff", Range(0, 1)) = 0.5
		_Tex7 ("Slot 7", 2D) = "white" {}
		_ST7 ("Slot 7 ST", Vector) = (1,1,0,0)
		_Color7 ("Slot 7 Color", Vector) = (1,1,1,1)
		_Cut7 ("Slot 7 Cutoff", Range(0, 1)) = 0.5
		_Tex8 ("Slot 8", 2D) = "white" {}
		_ST8 ("Slot 8 ST", Vector) = (1,1,0,0)
		_Color8 ("Slot 8 Color", Vector) = (1,1,1,1)
		_Cut8 ("Slot 8 Cutoff", Range(0, 1)) = 0.5
		_Tex9 ("Slot 9", 2D) = "white" {}
		_ST9 ("Slot 9 ST", Vector) = (1,1,0,0)
		_Color9 ("Slot 9 Color", Vector) = (1,1,1,1)
		_Cut9 ("Slot 9 Cutoff", Range(0, 1)) = 0.5
		_Tex10 ("Slot 10", 2D) = "white" {}
		_ST10 ("Slot 10 ST", Vector) = (1,1,0,0)
		_Color10 ("Slot 10 Color", Vector) = (1,1,1,1)
		_Cut10 ("Slot 10 Cutoff", Range(0, 1)) = 0.5
		_Tex11 ("Slot 11", 2D) = "white" {}
		_ST11 ("Slot 11 ST", Vector) = (1,1,0,0)
		_Color11 ("Slot 11 Color", Vector) = (1,1,1,1)
		_Cut11 ("Slot 11 Cutoff", Range(0, 1)) = 0.5
		_Tex12 ("Slot 12", 2D) = "white" {}
		_ST12 ("Slot 12 ST", Vector) = (1,1,0,0)
		_Color12 ("Slot 12 Color", Vector) = (1,1,1,1)
		_Cut12 ("Slot 12 Cutoff", Range(0, 1)) = 0.5
		_Tex13 ("Slot 13", 2D) = "white" {}
		_ST13 ("Slot 13 ST", Vector) = (1,1,0,0)
		_Color13 ("Slot 13 Color", Vector) = (1,1,1,1)
		_Cut13 ("Slot 13 Cutoff", Range(0, 1)) = 0.5
		_Tex14 ("Slot 14", 2D) = "white" {}
		_ST14 ("Slot 14 ST", Vector) = (1,1,0,0)
		_Color14 ("Slot 14 Color", Vector) = (1,1,1,1)
		_Cut14 ("Slot 14 Cutoff", Range(0, 1)) = 0.5
		_Tex15 ("Slot 15", 2D) = "white" {}
		_ST15 ("Slot 15 ST", Vector) = (1,1,0,0)
		_Color15 ("Slot 15 Color", Vector) = (1,1,1,1)
		_Cut15 ("Slot 15 Cutoff", Range(0, 1)) = 0.5
		[HideInInspector] _SrcBlend ("Src Blend", Float) = 1
		[HideInInspector] _DstBlend ("Dst Blend", Float) = 0
		[HideInInspector] _ZWrite ("ZWrite", Float) = 1
		[HideInInspector] _GhostAlpha ("Ghost Alpha (afterimage opacity)", Range(0, 1)) = 1
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
	Fallback "Mobile/CutoutShader"
}