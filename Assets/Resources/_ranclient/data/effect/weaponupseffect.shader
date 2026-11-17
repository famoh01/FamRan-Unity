Shader "Custom/WeaponEffect" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_GlossyEffect ("Glossy Effect", Range(0, 1)) = 1
		_DarkenAmount ("Darken Amount", Range(0, 1)) = 0.5
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.6
		[NoScaleOffset] _GlowTex ("Glow Texture", 2D) = "white" {}
		_GlowShineColor ("Glow Shine Color", Vector) = (1,1,1,1)
		_GlowShineStrength ("Glow Shine Strength", Range(0, 50)) = 30
		_GlowShineSpeed ("Glow Shine Speed", Range(0, 10)) = 5
		_GlowOpacity ("Glow Opacity", Range(0, 1)) = 0.4
		_ElementTex ("Element Texture", 2D) = "white" {}
		_ElementTex2 ("Element Texture 2", 2D) = "white" {}
		_ElementMoveDirection ("Element Move Direction", Vector) = (0.1,-0.1,0,0)
		_ElementOpacity ("Element Opacity", Range(0, 1)) = 0.3
		_ElementToggle ("Element Visibility Toggle", Range(0, 1)) = 0
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
	Fallback "Hidden/Universal Render Pipeline/FallbackError"
}