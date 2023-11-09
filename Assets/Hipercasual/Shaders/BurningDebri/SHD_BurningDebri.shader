// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/SHD_BurningDebri"
{
	Properties
	{
		_NoiseSize("Noise Size", Float) = 20
		_MainMaterial("MainMaterial", 2D) = "white" {}
		_SecondaryMaterial("SecondaryMaterial", 2D) = "white" {}
		_FireMaterial("FireMaterial", 2D) = "white" {}
		_FireEmission("FireEmission", Float) = 2
		_Width("Width", Range( 0 , 1)) = 1
		_Progress("Progress", Range( 0 , 1)) = 0.4897465
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _SecondaryMaterial;
		uniform float4 _SecondaryMaterial_ST;
		uniform sampler2D _MainMaterial;
		uniform float4 _MainMaterial_ST;
		uniform float _NoiseSize;
		uniform float _Progress;
		uniform float _Width;
		uniform sampler2D _FireMaterial;
		uniform float4 _FireMaterial_ST;
		uniform float _FireEmission;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_SecondaryMaterial = i.uv_texcoord * _SecondaryMaterial_ST.xy + _SecondaryMaterial_ST.zw;
			float2 uv_MainMaterial = i.uv_texcoord * _MainMaterial_ST.xy + _MainMaterial_ST.zw;
			float simplePerlin2D4 = snoise( i.uv_texcoord*_NoiseSize );
			simplePerlin2D4 = simplePerlin2D4*0.5 + 0.5;
			float Gradient8 = ( ( (-1.0 + (simplePerlin2D4 - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) * 0.3189708 ) + i.uv_texcoord.x );
			float NoiseIntensity7 = 0.3189708;
			float temp_output_24_0 = ( _Width * 0.5 );
			float temp_output_23_0 = ( ( NoiseIntensity7 * 0.5 ) + temp_output_24_0 );
			float temp_output_28_0 = (( 0.0 - temp_output_23_0 ) + (_Progress - 0.0) * (( temp_output_23_0 + 1.0 ) - ( 0.0 - temp_output_23_0 )) / (1.0 - 0.0));
			float temp_output_1_0_g1 = ( temp_output_28_0 - temp_output_24_0 );
			float temp_output_35_0 = ( ( Gradient8 - temp_output_1_0_g1 ) / ( ( temp_output_28_0 + temp_output_24_0 ) - temp_output_1_0_g1 ) );
			float MaterialSplit34 = saturate( temp_output_35_0 );
			float4 lerpResult17 = lerp( tex2D( _SecondaryMaterial, uv_SecondaryMaterial ) , tex2D( _MainMaterial, uv_MainMaterial ) , MaterialSplit34);
			float2 uv_FireMaterial = i.uv_texcoord * _FireMaterial_ST.xy + _FireMaterial_ST.zw;
			float4 temp_output_48_0 = ( tex2D( _FireMaterial, uv_FireMaterial ) * _FireEmission );
			float MidSection45 = saturate( ( 1.0 - abs( (-1.0 + (temp_output_35_0 - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) );
			float4 lerpResult51 = lerp( lerpResult17 , temp_output_48_0 , MidSection45);
			float4 Albedo52 = lerpResult51;
			o.Albedo = Albedo52.rgb;
			float4 Emissive54 = ( temp_output_48_0 * MidSection45 );
			o.Emission = Emissive54.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18934
0;0;1536;803;3312.904;1230.875;2.901856;False;False
Node;AmplifyShaderEditor.CommentaryNode;14;-2790.386,-860.8704;Inherit;False;1550.912;695.9141;Comment;10;8;5;13;12;10;4;2;3;7;11;Basic Noise Gradient;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;41;-2799.49,-113.0423;Inherit;False;2691.806;1151.886;Comment;2;42;44;Transition;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2316.506,-509.77;Inherit;False;Constant;_NoiseIntensity;Noise Intensity;1;0;Create;True;0;0;0;False;0;False;0.3189708;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;44;-2732.853,31.37992;Inherit;False;2016.202;648.063;Split Material;15;34;33;35;31;29;30;28;21;25;26;23;22;24;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;7;-2018.323,-281.6899;Inherit;False;NoiseIntensity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2666.33,-747.1489;Inherit;False;Property;_NoiseSize;Noise Size;0;0;Create;True;0;0;0;False;0;False;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2740.386,-604.2039;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;19;-2603.408,241.2978;Inherit;False;7;NoiseIntensity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;4;-2366.664,-810.8711;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2691.246,456.4858;Inherit;False;Property;_Width;Width;5;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;10;-2113.705,-746.368;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-2377.421,462.5677;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-2393.674,243.5568;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-2247.873,298.3459;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;13;-2427.409,-402.2685;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1847.205,-530.5696;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;26;-2061.594,234.1088;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-2058.417,340.9467;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2223.836,87.60182;Inherit;False;Property;_Progress;Progress;6;0;Create;True;0;0;0;False;0;False;0.4897465;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-1697.268,-430.425;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;28;-1871.205,187.2859;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;8;-1476.267,-421.3248;Inherit;False;Gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;29;-1660.697,396.7932;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-1662.697,520.7933;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-1604.396,254.8646;Inherit;False;8;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;35;-1455.279,426.0735;Inherit;True;Inverse Lerp;-1;;1;09cbe79402f023141a4dc1fddd4c9511;0;3;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;42;-1237.351,692.6219;Inherit;False;1106.033;324.005;Transition;5;37;40;39;38;45;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCRemapNode;37;-1187.351,742.6219;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;38;-906.2197,753.9568;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;33;-1105.294,415.1291;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;39;-702.1559,755.5258;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;-921.282,413.867;Inherit;False;MaterialSplit;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;40;-498.0949,768.0834;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;55;-1205.14,-1167.442;Inherit;False;1311.952;1035.38;Comment;12;17;18;15;16;46;51;54;52;53;48;50;47;Textures;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;15;-1180.449,-1102.982;Inherit;True;Property;_MainMaterial;MainMaterial;1;0;Create;True;0;0;0;False;0;False;-1;None;b3e3d0e5546494f43812fcb14cdc181b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-325.2771,777.6264;Inherit;False;MidSection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-1120.561,-647.7655;Inherit;False;34;MaterialSplit;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;47;-1097.118,-429.0089;Inherit;True;Property;_FireMaterial;FireMaterial;3;0;Create;True;0;0;0;False;0;False;-1;d62d5fba00e1dcd478d8d60875ac2218;d62d5fba00e1dcd478d8d60875ac2218;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-950.1177,-213.0083;Inherit;False;Property;_FireEmission;FireEmission;4;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-1177.372,-880.8891;Inherit;True;Property;_SecondaryMaterial;SecondaryMaterial;2;0;Create;True;0;0;0;False;0;False;-1;None;c0263a23283a0f74f9e27e91b4297dfe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;17;-778.2819,-803.3464;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-738.1176,-397.0089;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-764.9138,-578.2343;Inherit;False;45;MidSection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;51;-448.3207,-722.6901;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-396.1911,-432.4539;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;52;-103.4086,-678.49;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;54;-121.6948,-446.0641;Inherit;False;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;56;246.4124,-134.1761;Inherit;False;52;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;57;248.6148,-52.68261;Inherit;False;54;Emissive;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;582.3,-136.5;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Unlit/SHD_BurningDebri;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;11;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;10;0;4;0
WireConnection;24;0;20;0
WireConnection;22;0;19;0
WireConnection;23;0;22;0
WireConnection;23;1;24;0
WireConnection;13;0;2;1
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;26;1;23;0
WireConnection;25;0;23;0
WireConnection;5;0;12;0
WireConnection;5;1;13;0
WireConnection;28;0;21;0
WireConnection;28;3;26;0
WireConnection;28;4;25;0
WireConnection;8;0;5;0
WireConnection;29;0;28;0
WireConnection;29;1;24;0
WireConnection;30;0;28;0
WireConnection;30;1;24;0
WireConnection;35;1;29;0
WireConnection;35;2;30;0
WireConnection;35;3;31;0
WireConnection;37;0;35;0
WireConnection;38;0;37;0
WireConnection;33;0;35;0
WireConnection;39;0;38;0
WireConnection;34;0;33;0
WireConnection;40;0;39;0
WireConnection;45;0;40;0
WireConnection;17;0;16;0
WireConnection;17;1;15;0
WireConnection;17;2;18;0
WireConnection;48;0;47;0
WireConnection;48;1;50;0
WireConnection;51;0;17;0
WireConnection;51;1;48;0
WireConnection;51;2;46;0
WireConnection;53;0;48;0
WireConnection;53;1;46;0
WireConnection;52;0;51;0
WireConnection;54;0;53;0
WireConnection;0;0;56;0
WireConnection;0;2;57;0
ASEEND*/
//CHKSM=9587AB8F8FF60729379E3F486375EE56FAFDDC24