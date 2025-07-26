Shader "Unlit/waveup"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", float) = 0.1
        _Frequency ("Frequency", float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100
        Cull Off
        ZWrite Off
        Pass
        {
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _Speed;
            float _Frequency;
            
            float getWave(float2 uv) {
                float offset = sin(uv.x * _Frequency) * 0.009;
                return sin(((uv.y + offset) - _Time.y * _Speed) * _Frequency) + 1;
            }
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float waveVal = getWave(i.uv);

                if (i.normal.y != 0 ) discard;
                float4 gradient = lerp(float4(1,1,0,1), float4(0,0,0,1), i.uv.y) * 0.2;
                return float4(waveVal.xxx, 1) * gradient;
            }
            ENDCG
        }
    }
}
