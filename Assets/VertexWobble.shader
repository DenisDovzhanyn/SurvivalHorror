Shader "Unlit/VertexWobble"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _SnapAmp ("Vertex Snap Amplitude", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _SnapAmp;

            v2f vert (appdata v)
            {
                //* maybe _snapamp should be _gridsize? 
                //* the way i see it, we are multiplying by a grid size and then dividing by it so we can fine tune our vertex snapping 
                //* or another way to look at it is subdividing?
                v2f o;

                float4 viewSpace = mul(UNITY_MATRIX_MV, v.vertex);
                viewSpace.xyz = floor(viewSpace.xyz * _SnapAmp) / _SnapAmp;
            
                o.vertex = mul(UNITY_MATRIX_P, viewSpace);

                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // return (1,0,0,1);
            }
            ENDCG
        }
    }
}
