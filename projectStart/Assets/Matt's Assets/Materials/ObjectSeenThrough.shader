Shader "Custom/ObjectSeenThrough"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		Category{
		   Lighting On
		   ZTest Less
		   //ZWrite On
		   //Cull Front
		   SubShader {
			Tags {
			"Queue" = "Geometry+3000"
			//"RenderType" = "Opaque" 
	}
				Pass {
				   SetTexture[_MainTex] {
						constantColor[_Color]
						Combine texture * constant, texture * constant
					 }

				}
			}
	}
}
