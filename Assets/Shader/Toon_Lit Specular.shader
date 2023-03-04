Shader "Toon/Lit Specular" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_SColor ("Specular Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		_RampS ("Specular Ramp (RGB)", 2D) = "gray" {}
		_SpecSize ("Specular Size", Range(0.65, 0.999)) = 0.9
		_SpecOffset ("Specular Offset", Range(0.5, 1)) = 0.5
		_TColor ("Gradient Overlay Top Color", Vector) = (1,1,1,1)
		_BottomColor ("Gradient Overlay Bottom Color", Vector) = (0.23,0,0.95,1)
		_Offset ("Gradient Offset", Range(-4, 4)) = 3.2
		[Toggle(RIM)] _RIM ("Fresnel Rim?", Float) = 0
		_RimColor ("Fresnel Rim Color", Vector) = (0.49,0.94,0.64,1)
		[Toggle(FADE)] _FADE ("Fade specular to bottom?", Float) = 0
		_TopBottomOffset ("Specular Fade Offset", Range(-4, 4)) = 3.2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}