// Derived from 'Sprites-Diffuse.shader'
//
// Main changes:
//  - Main texture can be selected.
//  - Does not have color tinting.
//

Shader "Rotorz/Tileset/Transparent Diffuse"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma multi_compile DUMMY PIXELSNAP_ON

		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			v.normal = float3(0,0,-1);
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		ENDCG
	}

Fallback "Transparent/VertexLit"
}
