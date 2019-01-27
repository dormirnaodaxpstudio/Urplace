
uniform sampler2D _GrabTexture;

uniform float4 _Color;
uniform float4 _MainTex_ST;
uniform sampler2D _MainTex;

uniform float _Intensity;
uniform sampler2D _Refraction; 
uniform float4 _Refraction_ST;
uniform float _Current;

fixed4 refraction(sampler2D normalMap, float4 normalMap_ST, float2 texcoord, float4 screenPos, float intensity)
{
	screenPos /= screenPos.w;
	screenPos.y *= _ProjectionParams.x;
	
	#if UNITY_UV_STARTS_AT_TOP
		screenPos.y *= -_ProjectionParams.x;
	#else
		screenPos.y *= _ProjectionParams.x;
	#endif
	
	return tex2D(_GrabTexture, 0.5 + 0.5 * screenPos.xy + UnpackNormal(tex2D(normalMap, TRANSFORM_TEX(texcoord, normalMap))).xy * intensity);
}

float sinTransform(float2 texcoord, float4 wave)
{
	const float Pi2 = 2 * 3.14159;
	float w = 0.5 * (sin(Pi2 * (wave.x * texcoord.x + wave.z * _Time.y)) + 1);
	
	return texcoord.y + ((wave.w > 0 && wave.w < 1) ? wave.y * pow(w, wave.w) : wave.y * w);
}

fixed4 sampleCombined(sampler2D topTex, sampler2D mainTex, float4 mainTex_ST, float2 texcoord, float trim)
{
	float2 coord = float2(texcoord.x * mainTex_ST.x * mainTex_ST.y, texcoord.y * mainTex_ST.x + (1 - mainTex_ST.x));
	
	if (texcoord.y > 1 - 1/mainTex_ST.x)
		return (coord.y > trim) ? fixed4(0,0,0,0) : tex2D(topTex, coord);
	else
		return tex2D(mainTex, coord);
}
