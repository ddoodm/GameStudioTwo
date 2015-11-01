Shader "Unlit/PlasmaShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PlasSpeed("Plasma Frequency", Float) = 5.0
		_Tile("Plasma Size", Float) = 8.0
		_ColMult("Colour Multiplier", Color) = (1.0,1.0,1.0,1.0)
		_PlasPower("Plasma Power", Float) = 0.4
		_FalloffY("Falloff Height", Float) = 0.4
		_FalloffPower("Falloff Power", Float) = 0.5
		_Intensity("Intensity Multiplier", Float) = 4.0
		_FresnelPower("Fresnel(ish) Falloff Power", Float) = 1.0
		_Saturation("Saturation", Float) = 1.0
		_ZappyPow("Zappy Power", Float) = 50.0
		_ZappyMul("Zappy Colour Multiplier", Color) = (1.0,1.0,1.0,1.0)
		_ZappyIntens("Zappy Intensity", Float) = 1.0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Pass
		{
			Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// Make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 localPos : TEXCOORD3;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD4;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _PlasSpeed;
			float _PlasPower;
			float4 _ColMult;
			float _FalloffY;
			float _FalloffPower;
			float _Tile;
			float _Intensity;
			float _FresnelPower;
			float _Saturation;
			float _ZappyPow;
			float _ZappyIntens;
			fixed4 _ZappyMul;

			fixed getLumOf(float3 incol)
			{
				fixed3 targetChrom = fixed3(0.299, 0.587, 0.114);
				return dot(saturate(incol), targetChrom);
			}

			fixed4 dSaturate(fixed4 incol, fixed saturation)
			{
				fixed lum = getLumOf(incol);
				return lerp(lum, saturate(incol), saturation);
			}
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.normal = normalize(WorldSpaceViewDir(fixed4(v.normal, 1.0)));
				o.localPos = v.vertex;
				o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 plasmaFunc(fixed3 uvIn)
			{
				fixed tcx = uvIn.x, tcy = uvIn.y, tcz = uvIn.z;
				fixed tcxFnc = sin(tcx*tcy*3.3*_Tile + _Time*_PlasSpeed);
				fixed tcyFnc = cos(tcy + tcy*tcx*2.5*_Tile + _Time*_PlasSpeed);

				fixed plasFunc =
					0.75 + 0.25*sin(tan(tcxFnc) * (4 + sin(_Time*_PlasSpeed*1.1))*2.5 + _Time*_PlasSpeed*1.2);

				fixed3 outCol = fixed3(plasFunc*tcyFnc*0.4, plasFunc*tcxFnc*0.8, plasFunc);

				return fixed4(outCol, 1);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Sample the plasma function
				fixed4 col = saturate(plasmaFunc(i.localPos));
				col = pow(col, _PlasPower);

				// Sharp zappy stuff
				fixed zappyLum = getLumOf(pow(col, _ZappyPow).xyz);
				fixed4 zappy = _ZappyIntens * _ZappyMul * fixed4(zappyLum, zappyLum, zappyLum, 1.0);
				col += zappy;

				// Tint
				col *= _ColMult;

				// Falloff when the view direction ~= surface normal (Fresnel-ish)
				col *= pow(dot(i.normal, i.viewDir), _FresnelPower);

				// Falloff
				col *= max(0.0, i.uv.y - _FalloffY);
				col = pow(col, _FalloffPower);

				// Intensity multiplier
				col *= _Intensity;

				/* Viewspace test
				fixed4 worldPos = mul(_Object2World, i.localPos);
				float camAngle = dot(normalize(worldPos), normalize(worldPos - i.viewDir));
				col *= pow(0.0001, camAngle + 0.05);
				*/

				// (de)saturate
				col = dSaturate(col, _Saturation);

				// Apply Unity fog
				UNITY_APPLY_FOG(i.fogCoord, col);				
				return col;
			}
			ENDCG
		}
	}
}
