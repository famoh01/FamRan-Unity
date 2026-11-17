Shader "Custom/CombinedSlotsOverlay" {
	Properties {
		_SlotCount ("Slot Count", Float) = 1
		_GlowTex0 ("Glow 0", 2D) = "black" {}
		_GlowColor0 ("Glow Color 0", Vector) = (1,1,1,1)
		_GlowStrength0 ("Glow Strength 0", Range(0, 50)) = 30
		_GlowSpeed0 ("Glow Speed 0", Range(0, 10)) = 5
		_GlowOpacity0 ("Glow Opacity 0", Range(0, 1)) = 0.4
		_GlowTex1 ("Glow 1", 2D) = "black" {}
		_GlowColor1 ("Glow Color 1", Vector) = (1,1,1,1)
		_GlowStrength1 ("Glow Strength 1", Range(0, 50)) = 30
		_GlowSpeed1 ("Glow Speed 1", Range(0, 10)) = 5
		_GlowOpacity1 ("Glow Opacity 1", Range(0, 1)) = 0.4
		_GlowTex2 ("Glow 2", 2D) = "black" {}
		_GlowColor2 ("Glow Color 2", Vector) = (1,1,1,1)
		_GlowStrength2 ("Glow Strength 2", Range(0, 50)) = 30
		_GlowSpeed2 ("Glow Speed 2", Range(0, 10)) = 5
		_GlowOpacity2 ("Glow Opacity 2", Range(0, 1)) = 0.4
		_GlowTex3 ("Glow 3", 2D) = "black" {}
		_GlowColor3 ("Glow Color 3", Vector) = (1,1,1,1)
		_GlowStrength3 ("Glow Strength 3", Range(0, 50)) = 30
		_GlowSpeed3 ("Glow Speed 3", Range(0, 10)) = 5
		_GlowOpacity3 ("Glow Opacity 3", Range(0, 1)) = 0.4
		_GlowTex4 ("Glow 4", 2D) = "black" {}
		_GlowColor4 ("Glow Color 4", Vector) = (1,1,1,1)
		_GlowStrength4 ("Glow Strength 4", Range(0, 50)) = 30
		_GlowSpeed4 ("Glow Speed 4", Range(0, 10)) = 5
		_GlowOpacity4 ("Glow Opacity 4", Range(0, 1)) = 0.4
		_GlowTex5 ("Glow 5", 2D) = "black" {}
		_GlowColor5 ("Glow Color 5", Vector) = (1,1,1,1)
		_GlowStrength5 ("Glow Strength 5", Range(0, 50)) = 30
		_GlowSpeed5 ("Glow Speed 5", Range(0, 10)) = 5
		_GlowOpacity5 ("Glow Opacity 5", Range(0, 1)) = 0.4
		_GlowTex6 ("Glow 6", 2D) = "black" {}
		_GlowColor6 ("Glow Color 6", Vector) = (1,1,1,1)
		_GlowStrength6 ("Glow Strength 6", Range(0, 50)) = 30
		_GlowSpeed6 ("Glow Speed 6", Range(0, 10)) = 5
		_GlowOpacity6 ("Glow Opacity 6", Range(0, 1)) = 0.4
		_GlowTex7 ("Glow 7", 2D) = "black" {}
		_GlowColor7 ("Glow Color 7", Vector) = (1,1,1,1)
		_GlowStrength7 ("Glow Strength 7", Range(0, 50)) = 30
		_GlowSpeed7 ("Glow Speed 7", Range(0, 10)) = 5
		_GlowOpacity7 ("Glow Opacity 7", Range(0, 1)) = 0.4
		_GlowTex8 ("Glow 8", 2D) = "black" {}
		_GlowColor8 ("Glow Color 8", Vector) = (1,1,1,1)
		_GlowStrength8 ("Glow Strength 8", Range(0, 50)) = 30
		_GlowSpeed8 ("Glow Speed 8", Range(0, 10)) = 5
		_GlowOpacity8 ("Glow Opacity 8", Range(0, 1)) = 0.4
		_GlowTex9 ("Glow 9", 2D) = "black" {}
		_GlowColor9 ("Glow Color 9", Vector) = (1,1,1,1)
		_GlowStrength9 ("Glow Strength 9", Range(0, 50)) = 30
		_GlowSpeed9 ("Glow Speed 9", Range(0, 10)) = 5
		_GlowOpacity9 ("Glow Opacity 9", Range(0, 1)) = 0.4
		_GlowTex10 ("Glow 10", 2D) = "black" {}
		_GlowColor10 ("Glow Color 10", Vector) = (1,1,1,1)
		_GlowStrength10 ("Glow Strength 10", Range(0, 50)) = 30
		_GlowSpeed10 ("Glow Speed 10", Range(0, 10)) = 5
		_GlowOpacity10 ("Glow Opacity 10", Range(0, 1)) = 0.4
		_GlowTex11 ("Glow 11", 2D) = "black" {}
		_GlowColor11 ("Glow Color 11", Vector) = (1,1,1,1)
		_GlowStrength11 ("Glow Strength 11", Range(0, 50)) = 30
		_GlowSpeed11 ("Glow Speed 11", Range(0, 10)) = 5
		_GlowOpacity11 ("Glow Opacity 11", Range(0, 1)) = 0.4
		_GlowTex12 ("Glow 12", 2D) = "black" {}
		_GlowColor12 ("Glow Color 12", Vector) = (1,1,1,1)
		_GlowStrength12 ("Glow Strength 12", Range(0, 50)) = 30
		_GlowSpeed12 ("Glow Speed 12", Range(0, 10)) = 5
		_GlowOpacity12 ("Glow Opacity 12", Range(0, 1)) = 0.4
		_GlowTex13 ("Glow 13", 2D) = "black" {}
		_GlowColor13 ("Glow Color 13", Vector) = (1,1,1,1)
		_GlowStrength13 ("Glow Strength 13", Range(0, 50)) = 30
		_GlowSpeed13 ("Glow Speed 13", Range(0, 10)) = 5
		_GlowOpacity13 ("Glow Opacity 13", Range(0, 1)) = 0.4
		_GlowTex14 ("Glow 14", 2D) = "black" {}
		_GlowColor14 ("Glow Color 14", Vector) = (1,1,1,1)
		_GlowStrength14 ("Glow Strength 14", Range(0, 50)) = 30
		_GlowSpeed14 ("Glow Speed 14", Range(0, 10)) = 5
		_GlowOpacity14 ("Glow Opacity 14", Range(0, 1)) = 0.4
		_GlowTex15 ("Glow 15", 2D) = "black" {}
		_GlowColor15 ("Glow Color 15", Vector) = (1,1,1,1)
		_GlowStrength15 ("Glow Strength 15", Range(0, 50)) = 30
		_GlowSpeed15 ("Glow Speed 15", Range(0, 10)) = 5
		_GlowOpacity15 ("Glow Opacity 15", Range(0, 1)) = 0.4
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
}