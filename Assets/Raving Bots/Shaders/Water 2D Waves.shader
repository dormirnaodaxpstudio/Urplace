// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Water/Water 2D Waves"
{
	Properties 
	{
		[NoScaleOffset] _TopTex ("Top Texture", 2D) = "white" {}
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 0.5)
		[Normal] _Refraction ("Refraction", 2D) = "bump" {}
		_Intensity ("Refraction Intensity", Float) = 0.02
		_Current ("Current Speed", Float) = -0.25
		_Wave ("Wave", Vector) = (1, 0.2, 0.5, 0.7)
		_Level ("Level", Range (0, 1)) = 0.5
		_Trim ("Surface Trim", Range (0, 1)) = 0.9
	}
	SubShader 
	{
		Tags 
		{
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		GrabPass { }
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma multi_compile __ DISABLE_WAVES
			#pragma multi_compile __ DISABLE_REFRACTION
			
			#include "UnityCG.cginc"
			#include "Water2D.cginc"
			
			uniform float4 _Wave;
			uniform float _Level;
			
			uniform sampler2D _TopTex;
			
			uniform float _Trim;
			
			struct appdata_t 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float4 screenPos : TEXCOORD5;
			};
			
			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.texcoord = IN.texcoord;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.screenPos = OUT.vertex;
				
				return OUT;
			}
			
			fixed4 frag(v2f IN) : SV_Target
			{
				IN.texcoord.x += _Current * _Time.y;
			
			#if DISABLE_WAVES
				// no waves
			#else
				float t = clamp((IN.texcoord.y - _Level)/(1 - _Level), 0, 1);
				IN.texcoord.y = lerp(IN.texcoord.y, sinTransform(IN.texcoord, _Wave), t*t);
			#endif
			
				fixed4 texColor = sampleCombined(_TopTex, _MainTex, _MainTex_ST, IN.texcoord, _Trim);
				
			#if DISABLE_REFRACTION
				fixed4 c = texColor * _Color;
			#else
				fixed4 sceneColor = refraction(_Refraction, _Refraction_ST, IN.texcoord, IN.screenPos, _Intensity);
				fixed4 c = fixed4(lerp(sceneColor.rgb, texColor.rgb * _Color.rgb, _Color.a), texColor.a);
			#endif
				
				c.rgb *= c.a;
				
				return c;
			}
			ENDCG
		}
	}
}
