Shader "LD51/ScreenSpaceAlien"
{
    Properties
    {
    	_Color ("Color", Color) = (1, 1, 1, 1)
    	_ColorPat ("Pattern Color", Color) = (0, 0, 0, 1)
    	_ColorLgt ("Light Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _PatternTex ("Pattern Texture", 2D) = "white" {}
        _LightTex ("Light Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
   				float4 screenPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _PatternTex;
            float4 _PatternTex_ST;
            sampler2D _LightTex;
            fixed4 _Color;
            fixed4 _ColorPat;
            fixed4 _ColorLgt;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
     			o.screenPosition = ComputeScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 colMain = tex2D(_MainTex, i.uv);
                colMain *= _Color;
                fixed alpMain = colMain.a;

                // screen space effect
                float2 textureCoordinate = i.screenPosition.xy / i.screenPosition.w;
    			float aspect = _ScreenParams.x / _ScreenParams.y;
    			textureCoordinate.x = textureCoordinate.x * aspect;
    			float2 lightCoordinate = textureCoordinate;
    			textureCoordinate = TRANSFORM_TEX(textureCoordinate, _PatternTex);
    			fixed4 colPat = tex2D(_PatternTex, textureCoordinate);
    			fixed4 colLgtRaw = tex2D(_LightTex, lightCoordinate);
    			fixed4 colLgt = colLgtRaw * _ColorLgt;
    			colPat *= _ColorPat;
    			fixed alpPat = colPat.a;

    			fixed3 colComb = lerp(colMain.rgb, colPat.rgb, alpPat);

    			fixed3 colLgtRawTnt = colLgtRaw + _ColorLgt;
    			
    			fixed3 colCombAdd = colComb + colLgt;
    			fixed3 colCombMul = colCombAdd * colLgtRawTnt;

    			colComb = lerp(colComb, colCombMul, _ColorLgt.a);
    			fixed4 col = (1, 1, 1, 1);
    			col.rgb = colComb;
    			col.a = alpMain;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
