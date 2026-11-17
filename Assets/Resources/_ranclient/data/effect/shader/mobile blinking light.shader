Shader "Mobile/Blinking Light" {
	Properties {
		[Header(Base)] _MainTex ("Base Texture (PNG)", 2D) = "white" {}
		_Color ("Base Tint", Vector) = (1,1,1,1)
		_Cutoff ("Alpha Cutoff (0 = off)", Range(0, 1)) = 0
		[Header(Light)] [HDR] _EmissionColor ("Light Color (HDR)", Vector) = (1,0.8,0.4,1)
		_EmissionMask ("Emission Mask (optional, white = glows)", 2D) = "white" {}
		_MinIntensity ("Min Intensity (off level)", Range(0, 4)) = 0
		_MaxIntensity ("Max Intensity (on level)", Range(0, 8)) = 2
		[Header(Blink)] [KeywordEnum(Smooth, Hard, Flicker)] _Blink ("Blink Mode", Float) = 0
		[KeywordEnum(Emission, Whole)] _Target ("Blink Target", Float) = 0
		_Speed ("Speed (blinks per second)", Range(0, 20)) = 1
		_Duty ("Duty / On Fraction (Hard mode)", Range(0.05, 0.95)) = 0.5
		_Phase ("Phase Offset (cycles)", Range(0, 1)) = 0
		_FlickerAmount ("Flicker Amount (Flicker mode)", Range(0, 1)) = 0.6
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
			float4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy) * _Color;
			}

			ENDHLSL
		}
	}
}