Shader "Shader Graphs/EffectCharacterLevel" {
	Properties {
		_Material ("Material", 2D) = "black" {}
		[NoScaleOffset] _EffectTexture ("EffectTexture", 2D) = "white" {}
		_Power ("Power", Float) = 2
		[HDR] _Color ("Color", Vector) = (1.498039,0.2588235,0.08627451,1)
		_Speed ("Speed", Vector) = (0,0.1,0,0)
		[ToggleUI] _emission ("emission", Float) = 0
		_BaseColor ("BaseColor", Vector) = (0,0,0,1)
		[HideInInspector] _BUILTIN_Surface ("Float", Float) = 0
		[HideInInspector] _BUILTIN_Blend ("Float", Float) = 2
		[HideInInspector] _BUILTIN_AlphaClip ("Float", Float) = 1
		[HideInInspector] _BUILTIN_SrcBlend ("Float", Float) = 1
		[HideInInspector] _BUILTIN_DstBlend ("Float", Float) = 0
		[HideInInspector] _BUILTIN_ZWrite ("Float", Float) = 1
		[HideInInspector] _BUILTIN_ZWriteControl ("Float", Float) = 1
		[HideInInspector] _BUILTIN_ZTest ("Float", Float) = 4
		[HideInInspector] _BUILTIN_CullMode ("Float", Float) = 2
		[HideInInspector] _BUILTIN_QueueOffset ("Float", Float) = 0
		[HideInInspector] _BUILTIN_QueueControl ("Float", Float) = -1
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
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}