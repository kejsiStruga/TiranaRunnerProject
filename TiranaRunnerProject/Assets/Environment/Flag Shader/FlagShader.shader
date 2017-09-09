// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CustomSprites/FlagShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Curves("Cureves",Range(0,10)) = 0.5
		_gravity("Gravity Fall",Range(0,1)) = 0.5
		_windSpeed("Wind Speed",Range(0,25)) = 4
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			//"IgnoreProjector" = "True"
			//"RenderType" = "Transparent"
			//"PreviewType" = "Plane"
			//"CanUseSpriteAtlas" = "True"
		}

			Cull Off
			Lighting Off
			ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			
			//Blend One OneMinusSrcAlpha


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _Curves, _gravity, _windSpeed;
			
			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 ProcessForAnimation(v2f i) {
				float yoffset = i.uv.x - _Time.x * _windSpeed; // Animates Root of Flag as Well,wind speed
				yoffset = (yoffset * 2) - 1;
				yoffset = yoffset*1.57079633 * _Curves;	//one cycle of trinometric function, curves controll
				yoffset = cos(yoffset * 2)*i.uv.x*0.05 + _gravity*i.uv.x*i.uv.x;
				float2 uv = float2(i.uv.x, (i.uv.y + yoffset));
				//uv = (uv + 1) / 2;
				fixed4 col = tex2D(_MainTex, uv);
				return col;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = ProcessForAnimation(i);
				col.rgb *= col.a;
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);				
				return col;
			}
				
			ENDCG
		}
	}
}
