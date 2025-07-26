Shader "Unlit/shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Wave Speed", float) = 0.1
        _Amplitude ("Amplitude", float) = 1
        _Height ("Height", float) = 1
    }   
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull false
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct meshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct interpolated
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _Speed;
            float _Amplitude;
            float _Height;
            
            float getHeight(float2 uv) {
                float dist = length (uv);

                return (sin((dist - _Time.y * _Speed) * _Amplitude) * _Height) * 1-dist;
            }

            interpolated vert (meshData v)
            {
                interpolated o;
                v.uv = v.uv * 2 - 1;
                v.vertex.y += getHeight(v.uv);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = v.uv;
                return o;
            }

            float4 frag (interpolated i) : SV_Target
            {
                float blue = lerp(0.1, 1, saturate(getHeight(i.uv)));
                return float4(0,0,blue, 1);
            }

            
            ENDCG
        }
    }
}
