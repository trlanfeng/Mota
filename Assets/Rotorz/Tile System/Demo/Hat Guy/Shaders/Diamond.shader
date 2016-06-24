// Based on "http://wiki.unity3d.com/index.php?title=IPhoneGems"

Shader "FX/Diamond" {
Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_Emission ("Emissive", Color) = (1,1,1,1)
	_Fog("Fog", Color) = (0,0,0,0)
	
	_RefractTexlow ("Refraction", 2D) = "dummy.jpg" { TexGen SphereMap }
	_ReflectTexlow ("Reflect", 2D) = "dummy.jpg" { TexGen SphereMap }
}   

SubShader {
	Pass {      
		Lighting Off
		
		Color (0,0,0,0)
		Cull Front
		
		SetTexture [_ReflectTexlow] {
			constantColor [_Color]
			combine texture * constant, primary
		}
	}
	
	// Second pass - here we render the front faces of the diamonds.
	Pass {
		Fog { Color [_Fog]}
		ZWrite on
		Blend One One
		
		SetTexture [_RefractTexlow] {
			constantColor [_Emission]
			combine texture * constant
		}
	}  
}
}