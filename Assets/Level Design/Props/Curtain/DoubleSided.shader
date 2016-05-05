Shader "DoubleSided" {
	Properties{
		_Color("Color", Color) = (1,1,1)
	}
		SubShader{
		Lighting On
		Material{
		Ambient[_Color]
		Diffuse[_Color]
	}

		Pass{
		Cull Off
	}
	}
}