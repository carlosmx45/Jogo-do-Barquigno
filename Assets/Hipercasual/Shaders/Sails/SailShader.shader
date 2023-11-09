// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SailShader"
{
	Properties
	{
		_Mask("Mask", 2D) = "white" {}
		_VoronoiStrength("Voronoi Strength", Int) = 2
		_VoronoiScale("Voronoi Scale", Int) = 1
		_VoronoiSeed("VoronoiSeed", Int) = 0
		_VoronoiPanningSpeed("Voronoi Panning Speed", Int) = 2
		_SailFillPercent("SailFillPercent", Range( 0 , 1)) = 1
		_FlappinDirection("FlappinDirection", Vector) = (0,0,1,0)
		_Albedo("Albedo", 2D) = "white" {}
		_SailWindLevel("SailWindLevel", Float) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _SailWindLevel;
		uniform float _SailFillPercent;
		uniform float3 _FlappinDirection;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform int _VoronoiScale;
		uniform int _VoronoiSeed;
		uniform int _VoronoiPanningSpeed;
		uniform int _VoronoiStrength;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Smoothness;


		float2 voronoihash1( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi1( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash1( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			
			 		}
			 	}
			}
			return F1;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float _sailWindLevel16 = ( _SailWindLevel * _SailFillPercent );
			float3 temp_output_16_0_g15 = _FlappinDirection;
			float2 uv_Mask = v.texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float temp_output_19_0_g15 = tex2Dlod( _Mask, float4( uv_Mask, 0, 0.0) ).r;
			float time1 = ( _VoronoiSeed + ( _VoronoiPanningSpeed * _Time.y ) );
			float2 voronoiSmoothId1 = 0;
			float2 coords1 = v.texcoord.xy * (float)_VoronoiScale;
			float2 id1 = 0;
			float2 uv1 = 0;
			float voroi1 = voronoi1( coords1, time1, id1, uv1, 0, voronoiSmoothId1 );
			float _panningVoronoi7 = ( voroi1 * _VoronoiStrength );
			float3 vertexOffset51 = ( ( ( _sailWindLevel16 * temp_output_16_0_g15 ) * temp_output_19_0_g15 ) + ( temp_output_19_0_g15 * temp_output_16_0_g15 * _panningVoronoi7 ) );
			v.vertex.xyz += vertexOffset51;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18934
0;0;1920;1011;3664.875;74.74621;1.204487;False;False
Node;AmplifyShaderEditor.CommentaryNode;2;-2612.026,430.9734;Inherit;False;1236.153;560.5859;;10;7;6;1;19;4;18;5;21;3;17;Panning Voronoi;1,1,1,1;0;0
Node;AmplifyShaderEditor.IntNode;18;-2598.809,604.8204;Inherit;False;Property;_VoronoiPanningSpeed;Voronoi Panning Speed;4;0;Create;True;0;0;0;False;0;False;2;2;False;0;1;INT;0
Node;AmplifyShaderEditor.TimeNode;5;-2562.282,713.0717;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-2337.26,644.6285;Inherit;False;2;2;0;INT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;17;-2407.644,498.3596;Inherit;False;Property;_VoronoiSeed;VoronoiSeed;3;0;Create;True;0;0;0;False;0;False;0;0;False;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;19;-2306.809,778.8198;Inherit;False;Property;_VoronoiScale;Voronoi Scale;2;0;Create;True;0;0;0;False;0;False;1;1;False;0;1;INT;0
Node;AmplifyShaderEditor.CommentaryNode;9;-3445.429,64.85485;Inherit;False;775.3044;269.7606;;4;16;10;13;39;Inputs;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-2179.151,551.1351;Inherit;False;2;2;0;INT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;21;-2042.767,893.4814;Inherit;False;Property;_VoronoiStrength;Voronoi Strength;1;0;Create;True;0;0;0;False;0;False;2;2;False;0;1;INT;0
Node;AmplifyShaderEditor.VoronoiNode;1;-2025.18,599.4371;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;39;-3301.549,146.3875;Inherit;False;Property;_SailWindLevel;SailWindLevel;8;0;Create;True;0;0;0;False;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-3405.043,251.6199;Inherit;False;Property;_SailFillPercent;SailFillPercent;5;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1779.476,767.4917;Inherit;False;2;2;0;FLOAT;0;False;1;INT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-3057.042,191.6201;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-1578.48,761.6037;Inherit;False;_panningVoronoi;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-2878.042,185.6201;Inherit;False;_sailWindLevel;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;22;-3596.775,429.6967;Inherit;False;929.3865;678.3782;;6;51;55;25;26;34;33;Vertex Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;25;-3481.379,501.7129;Inherit;False;Property;_FlappinDirection;FlappinDirection;6;0;Create;True;0;0;0;False;0;False;0,0,1;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;26;-3500.379,673.7132;Inherit;False;7;_panningVoronoi;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-3487.352,987.1608;Inherit;False;16;_sailWindLevel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;33;-3577.352,775.1608;Inherit;True;Property;_Mask;Mask;0;0;Create;True;0;0;0;False;0;False;-1;None;8fa7f5d94008bdc40a9da7d679e46eb5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;55;-3186.956,703.0672;Inherit;False;Sail;-1;;15;ba0ea5852497b484ca002ca4ff41a301;0;4;19;FLOAT;0;False;16;FLOAT3;0,0,0;False;17;FLOAT;0;False;18;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;51;-2850.87,697.3268;Inherit;False;vertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;-2534.733,283.7442;Inherit;False;51;vertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2622.148,190.6878;Inherit;False;Property;_Smoothness;Smoothness;9;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;52;-2641.457,-18.92302;Inherit;True;Property;_Albedo;Albedo;7;0;Create;True;0;0;0;False;0;False;-1;None;2fe3b5b03e805984fb2755526258ad99;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-2307.336,-12.96142;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SailShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;18;0
WireConnection;4;1;5;2
WireConnection;3;0;17;0
WireConnection;3;1;4;0
WireConnection;1;1;3;0
WireConnection;1;2;19;0
WireConnection;6;0;1;0
WireConnection;6;1;21;0
WireConnection;10;0;39;0
WireConnection;10;1;13;0
WireConnection;7;0;6;0
WireConnection;16;0;10;0
WireConnection;55;19;33;0
WireConnection;55;16;25;0
WireConnection;55;17;34;0
WireConnection;55;18;26;0
WireConnection;51;0;55;0
WireConnection;0;0;52;0
WireConnection;0;4;53;0
WireConnection;0;11;54;0
ASEEND*/
//CHKSM=12D5172E613A1F8427EC726C1A184BDCDD89EBED