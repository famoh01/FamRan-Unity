Shader "Mobile/Scrolling Overlay" {
	Properties {
		[Header(Guide)] _MainTex ("Guide Texture (alpha = where overlays show)", 2D) = "white" {}
		_MaskSoftness ("Edge Softness", Range(0, 1)) = 1
		_Cutoff ("Alpha Cutoff (0 = off)", Range(0, 1)) = 0
		[Header(Overlay 1)] [Toggle] _Enable1 ("Overlay 1 On", Float) = 1
		_OverlayTex ("Overlay 1 Texture (PNG)", 2D) = "black" {}
		[Toggle] _KeepDark1 ("Overlay 1 Keep Dark Colors (PNG alpha only)", Float) = 1
		[HDR] _OverlayColor ("Overlay 1 Tint", Vector) = (1,1,1,1)
		_OverlayStrength ("Overlay 1 Strength", Range(0, 4)) = 1
		_OverlayTiling ("Overlay 1 Tiling (X, Y)", Vector) = (1,1,0,0)
		_ScrollSpeed ("Overlay 1 Scroll Speed (X, Y)", Vector) = (0.25,0,0,0)
		_OverlayRotation ("Overlay 1 Rotation (deg)", Range(0, 360)) = 0
		[Header(Overlay 2)] [Toggle] _Enable2 ("Overlay 2 On", Float) = 0
		_OverlayTex2 ("Overlay 2 Texture (PNG)", 2D) = "black" {}
		[Toggle] _KeepDark2 ("Overlay 2 Keep Dark Colors (PNG alpha only)", Float) = 1
		[HDR] _OverlayColor2 ("Overlay 2 Tint", Vector) = (1,1,1,1)
		_OverlayStrength2 ("Overlay 2 Strength", Range(0, 4)) = 1
		_OverlayTiling2 ("Overlay 2 Tiling (X, Y)", Vector) = (1,1,0,0)
		_ScrollSpeed2 ("Overlay 2 Scroll Speed (X, Y)", Vector) = (0,0.25,0,0)
		_OverlayRotation2 ("Overlay 2 Rotation (deg)", Range(0, 360)) = 0
		[KeywordEnum(Additive, Alpha, Multiply)] _Overlay2 ("Overlay 2 Blend (over layer 1)", Float) = 0
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend", Float) = 10
		[Enum(Off, 0, On, 1)] _ZWrite ("ZWrite", Float) = 0
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
			float4 _MainTex_ST;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy);
			}

			ENDHLSL
		}
	}
}