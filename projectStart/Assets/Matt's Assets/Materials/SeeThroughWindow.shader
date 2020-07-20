Shader "Custom/SeeThroughWindow"
{
	SubShader{
	Tags {"Queue" = "Geometry+2999" }
	Lighting Off
		ZTest LEqual
		ZWrite On
		ColorMask 0
		Pass {}
	}
}
