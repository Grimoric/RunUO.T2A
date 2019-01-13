/***************************************************************************
 *                                Utility.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id: Utility.cs 1065 2013-06-02 13:12:09Z eos@runuo.com $
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace Server
{
    public static class Utility
	{
		private static Random m_Random = new Random();
		private static Encoding m_UTF8, m_UTF8WithEncoding;

		public static Encoding UTF8
		{
			get
			{
				if ( m_UTF8 == null )
					m_UTF8 = new UTF8Encoding( false, false );

				return m_UTF8;
			}
		}

		public static Encoding UTF8WithEncoding
		{
			get
			{
				if ( m_UTF8WithEncoding == null )
					m_UTF8WithEncoding = new UTF8Encoding( true, false );

				return m_UTF8WithEncoding;
			}
		}

		public static void Separate( StringBuilder sb, string value, string separator )
		{
			if ( sb.Length > 0 )
				sb.Append( separator );

			sb.Append( value );
		}

		public static string Intern( string str )
		{
			if ( str == null )
				return null;
			else if ( str.Length == 0 )
				return String.Empty;

			return String.Intern( str );
		}

		public static void Intern( ref string str )
		{
			str = Intern( str );
		}

		private static Dictionary<IPAddress, IPAddress> _ipAddressTable;

		public static IPAddress Intern( IPAddress ipAddress ) {
			if ( _ipAddressTable == null ) {
				_ipAddressTable = new Dictionary<IPAddress, IPAddress>();
			}

			IPAddress interned;

			if ( !_ipAddressTable.TryGetValue( ipAddress, out interned ) ) {
				interned = ipAddress;
				_ipAddressTable[ipAddress] = interned;
			}

			return interned;
		}

		public static bool IsValidIP( string text )
		{
			bool valid = true;

			IPMatch( text, IPAddress.None, ref valid );

			return valid;
		}

		public static bool IPMatch( string val, IPAddress ip )
		{
			bool valid = true;

			return IPMatch( val, ip, ref valid );
		}

		public static string FixHtml( string str )
		{
			if( str == null )
				return "";

			bool hasOpen  = str.IndexOf( '<' ) >= 0;
			bool hasClose = str.IndexOf( '>' ) >= 0;
			bool hasPound = str.IndexOf( '#' ) >= 0;

			if ( !hasOpen && !hasClose && !hasPound )
				return str;

			StringBuilder sb = new StringBuilder( str );

			if ( hasOpen )
				sb.Replace( '<', '(' );

			if ( hasClose )
				sb.Replace( '>', ')' );

			if ( hasPound )
				sb.Replace( '#', '-' );

			return sb.ToString();
		}

		public static bool IPMatchCIDR( IPAddress cidrPrefix, IPAddress ip, int cidrLength )
		{
			if ( cidrPrefix == null || ip == null || cidrPrefix.AddressFamily == AddressFamily.InterNetworkV6 )	//Ignore IPv6 for now
				return false;

			uint cidrValue = SwapUnsignedInt( (uint)GetLongAddressValue( cidrPrefix ) );
			uint ipValue   = SwapUnsignedInt( (uint)GetLongAddressValue( ip ) );

			return IPMatchCIDR( cidrValue, ipValue, cidrLength );
		}

		public static bool IPMatchCIDR( uint cidrPrefixValue, uint ipValue, int cidrLength )
		{
			if ( cidrLength <= 0 || cidrLength >= 32 )   //if invalid cidr Length, just compare IPs
				return cidrPrefixValue == ipValue;

			uint mask = uint.MaxValue << 32-cidrLength;

			return ( cidrPrefixValue & mask ) == ( ipValue & mask );
		}

		private static uint SwapUnsignedInt( uint source )
		{
			return (uint)( ( ( source & 0x000000FF ) << 0x18 )
			               | ( ( source & 0x0000FF00 ) << 8 )
			               | ( ( source & 0x00FF0000 ) >> 8 )
			               | ( ( source & 0xFF000000 ) >> 0x18 ) );
		} 

		public static bool IPMatch( string val, IPAddress ip, ref bool valid )
		{
			valid = true;

			string[] split = val.Split( '.' );

			for ( int i = 0; i < 4; ++i )
			{
				int lowPart, highPart;

				if ( i >= split.Length )
				{
					lowPart = 0;
					highPart = 255;
				}
				else
				{
					string pattern = split[i];

					if ( pattern == "*" )
					{
						lowPart = 0;
						highPart = 255;
					}
					else
					{
						lowPart = 0;
						highPart = 0;

						bool highOnly = false;
						int lowBase = 10;
						int highBase = 10;

						for ( int j = 0; j < pattern.Length; ++j )
						{
							char c = (char)pattern[j];

							if ( c == '?' )
							{
								if ( !highOnly )
								{
									lowPart *= lowBase;
									lowPart += 0;
								}

								highPart *= highBase;
								highPart += highBase - 1;
							}
							else if ( c == '-' )
							{
								highOnly = true;
								highPart = 0;
							}
							else if ( c == 'x' || c == 'X' )
							{
								lowBase = 16;
								highBase = 16;
							}
							else if ( c >= '0' && c <= '9' )
							{
								int offset = c - '0';

								if ( !highOnly )
								{
									lowPart *= lowBase;
									lowPart += offset;
								}

								highPart *= highBase;
								highPart += offset;
							}
							else if ( c >= 'a' && c <= 'f' )
							{
								int offset = 10 + (c - 'a');

								if ( !highOnly )
								{
									lowPart *= lowBase;
									lowPart += offset;
								}

								highPart *= highBase;
								highPart += offset;
							}
							else if ( c >= 'A' && c <= 'F' )
							{
								int offset = 10 + (c - 'A');

								if ( !highOnly )
								{
									lowPart *= lowBase;
									lowPart += offset;
								}

								highPart *= highBase;
								highPart += offset;
							}
							else
							{
								valid = false;	//high & lowpart would be 0 if it got to here.
							}
						}
					}
				}

				int b = (byte)(Utility.GetAddressValue( ip ) >> (i * 8));

				if ( b < lowPart || b > highPart )
					return false;
			}

			return true;
		}

		public static bool IPMatchClassC( IPAddress ip1, IPAddress ip2 )
		{
			return (Utility.GetAddressValue( ip1 ) & 0xFFFFFF) == (Utility.GetAddressValue( ip2 ) & 0xFFFFFF);
		}

		public static int InsensitiveCompare( string first, string second )
		{
			return Insensitive.Compare( first, second );
		}

		public static bool InsensitiveStartsWith( string first, string second )
		{
			return Insensitive.StartsWith( first, second );
		}

		#region To[Something]
		public static bool ToBoolean( string value )
		{
			bool b;
			bool.TryParse( value, out b );

			return b;
		}

		public static double ToDouble( string value )
		{
			double d;
			double.TryParse( value, out d );

			return d;
		}

		public static TimeSpan ToTimeSpan( string value )
		{
			TimeSpan t;
			TimeSpan.TryParse( value, out t );

			return t;
		}

		public static int ToInt32( string value )
		{
			int i;

			if( value.StartsWith( "0x" ) )
				int.TryParse( value.Substring( 2 ), NumberStyles.HexNumber, null, out i );
			else
				int.TryParse( value, out i );

			return i;
		}
		#endregion

		#region Get[Something]
		public static int GetXMLInt32( string intString, int defaultValue )
		{
			try
			{
				return XmlConvert.ToInt32( intString );
			}
			catch
			{
				int val;
				if ( int.TryParse( intString, out val ) )
					return val;

				return defaultValue;
			}
		}

		public static DateTime GetXMLDateTime( string dateTimeString, DateTime defaultValue )
		{
			try
			{
				return XmlConvert.ToDateTime( dateTimeString, XmlDateTimeSerializationMode.Local );
			}
			catch
			{
				DateTime d;

				if( DateTime.TryParse( dateTimeString, out d ) )
					return d;

				return defaultValue;
			}
		}

		public static TimeSpan GetXMLTimeSpan( string timeSpanString, TimeSpan defaultValue )
		{
			try
			{
				return XmlConvert.ToTimeSpan( timeSpanString );
			}
			catch
			{
				return defaultValue;
			}
		}

		public static string GetAttribute( XmlElement node, string attributeName )
		{
			return GetAttribute( node, attributeName, null );
		}

		public static string GetAttribute( XmlElement node, string attributeName, string defaultValue )
		{
			if ( node == null )
				return defaultValue;

			XmlAttribute attr = node.Attributes[attributeName];

			if ( attr == null )
				return defaultValue;

			return attr.Value;
		}

		public static string GetText( XmlElement node, string defaultValue )
		{
			if ( node == null )
				return defaultValue;

			return node.InnerText;
		}

		public static int GetAddressValue( IPAddress address )
		{
#pragma warning disable 618
			return (int)address.Address;
#pragma warning restore 618
		}

		public static long GetLongAddressValue( IPAddress address )
		{
#pragma warning disable 618
			return address.Address;
#pragma warning restore 618
		}
		#endregion

		public static double RandomDouble()
		{
			return m_Random.NextDouble();
		}
		#region In[...]Range
		public static bool InRange( Point3D p1, Point3D p2, int range )
		{
			return p1.m_X >= p2.m_X - range
				&& p1.m_X <= p2.m_X + range
				&& p1.m_Y >= p2.m_Y - range
				&& p1.m_Y <= p2.m_Y + range;
		}

		public static bool InUpdateRange( Point3D p1, Point3D p2 )
		{
			return p1.m_X >= p2.m_X - 18
				&& p1.m_X <= p2.m_X + 18
				&& p1.m_Y >= p2.m_Y - 18
				&& p1.m_Y <= p2.m_Y + 18;
		}

		public static bool InUpdateRange( IPoint2D p1, IPoint2D p2 )
		{
			return p1.X >= p2.X - 18
				&& p1.X <= p2.X + 18
				&& p1.Y >= p2.Y - 18
				&& p1.Y <= p2.Y + 18;
		}

		#endregion
		public static Direction GetDirection( IPoint2D from, IPoint2D to )
		{
			int dx = to.X - from.X;
			int dy = to.Y - from.Y;

			int adx = Math.Abs( dx );
			int ady = Math.Abs( dy );

			if ( adx >= ady * 3 )
			{
				if ( dx > 0 )
					return Direction.East;
				else
					return Direction.West;
			}
			else if ( ady >= adx * 3 )
			{
				if ( dy > 0 )
					return Direction.South;
				else
					return Direction.North;
			}
			else if ( dx > 0 )
			{
				if ( dy > 0 )
					return Direction.Down;
				else
					return Direction.Right;
			}
			else
			{
				if ( dy > 0 )
					return Direction.Left;
				else
					return Direction.Up;
			}
		}

		//4d6+8 would be: Utility.Dice( 4, 6, 8 )
		public static int Dice( int numDice, int numSides, int bonus )
		{
			int total = 0;
			for (int i=0;i<numDice;++i)
				total += Random( numSides ) + 1;
			total += bonus;
			return total;
		}

		public static int RandomList( params int[] list )
		{
			return list[m_Random.Next( list.Length )];
		}

		public static bool RandomBool()
		{
			return m_Random.Next( 2 ) == 0;
		}

		public static int RandomMinMax( int min, int max )
		{
			if ( min > max )
			{
				int copy = min;
				min = max;
				max = copy;
			}
			else if ( min == max )
			{
				return min;
			}

			return min + m_Random.Next( max - min + 1 );
		}

		public static int Random( int from, int count )
		{
			if ( count == 0 )
			{
				return from;
			}
			else if ( count > 0 )
			{
				return from + m_Random.Next( count );
			}
			else
			{
				return from - m_Random.Next( -count );
			}
		}

		public static int Random( int count )
		{
			return m_Random.Next( count );
		}

		public static void RandomBytes( byte[] buffer )
		{
			m_Random.NextBytes( buffer );
		}

		#region Random Hues

		/// <summary>
		/// Random pink, blue, green, orange, red or yellow hue
		/// </summary>
		public static int RandomNondyedHue()
		{
			switch ( Random( 6 ) )
			{
				case 0: return RandomPinkHue();
				case 1: return RandomBlueHue();
				case 2: return RandomGreenHue();
				case 3: return RandomOrangeHue();
				case 4: return RandomRedHue();
				case 5: return RandomYellowHue();
			}

			return 0;
		}

		/// <summary>
		/// Random hue in the range 1201-1254
		/// </summary>
		public static int RandomPinkHue()
		{
			return Random( 1201, 54 );
		}

		/// <summary>
		/// Random hue in the range 1301-1354
		/// </summary>
		public static int RandomBlueHue()
		{
			return Random( 1301, 54 );
		}

		/// <summary>
		/// Random hue in the range 1401-1454
		/// </summary>
		public static int RandomGreenHue()
		{
			return Random( 1401, 54 );
		}

		/// <summary>
		/// Random hue in the range 1501-1554
		/// </summary>
		public static int RandomOrangeHue()
		{
			return Random( 1501, 54 );
		}

		/// <summary>
		/// Random hue in the range 1601-1654
		/// </summary>
		public static int RandomRedHue()
		{
			return Random( 1601, 54 );
		}

		/// <summary>
		/// Random hue in the range 1701-1754
		/// </summary>
		public static int RandomYellowHue()
		{
			return Random( 1701, 54 );
		}

		/// <summary>
		/// Random hue in the range 1801-1908
		/// </summary>
		public static int RandomNeutralHue()
		{
			return Random( 1801, 108 );
		}

		/// <summary>
		/// Random hue in the range 2001-2018
		/// </summary>
		public static int RandomSnakeHue()
		{
			return Random( 2001, 18 );
		}

		/// <summary>
		/// Random hue in the range 2101-2130
		/// </summary>
		public static int RandomBirdHue()
		{
			return Random( 2101, 30 );
		}

		/// <summary>
		/// Random hue in the range 2201-2224
		/// </summary>
		public static int RandomSlimeHue()
		{
			return Random( 2201, 24 );
		}

		/// <summary>
		/// Random hue in the range 2301-2318
		/// </summary>
		public static int RandomAnimalHue()
		{
			return Random( 2301, 18 );
		}

		/// <summary>
		/// Random hue in the range 2401-2430
		/// </summary>
		public static int RandomMetalHue()
		{
			return Random( 2401, 30 );
		}

		public static int ClipDyedHue( int hue )
		{
			if ( hue < 2 )
				return 2;
			else if ( hue > 1001 )
				return 1001;
			else
				return hue;
		}

		/// <summary>
		/// Random hue in the range 2-1001
		/// </summary>
		public static int RandomDyedHue()
		{
			return Random( 2, 1000 );
		}

		/// <summary>
		/// Random hue from 0x62, 0x71, 0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59
		/// </summary>
		public static int RandomBrightHue()
		{
			if ( Utility.RandomDouble() < 0.1  )
				return Utility.RandomList( 0x62, 0x71 );

			return Utility.RandomList( 0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59 );
		}

		//[Obsolete( "Depreciated, use the methods for the Mobile's race", false )]
		public static int RandomSkinHue()
		{
			return Random( 1002, 57 ) | 0x8000;
		}

		//[Obsolete( "Depreciated, use the methods for the Mobile's race", false )]
		public static int RandomHairHue()
		{
			return Random( 1102, 48 );
		}

		#endregion

		private static SkillName[] m_AllSkills = new SkillName[]
			{
				SkillName.Alchemy,
				SkillName.Anatomy,
				SkillName.AnimalLore,
				SkillName.ItemID,
				SkillName.ArmsLore,
				SkillName.Parry,
				SkillName.Begging,
				SkillName.Blacksmith,
				SkillName.Fletching,
				SkillName.Peacemaking,
				SkillName.Camping,
				SkillName.Carpentry,
				SkillName.Cartography,
				SkillName.Cooking,
				SkillName.DetectHidden,
				SkillName.Discordance,
				SkillName.EvalInt,
				SkillName.Healing,
				SkillName.Fishing,
				SkillName.Forensics,
				SkillName.Herding,
				SkillName.Hiding,
				SkillName.Provocation,
				SkillName.Inscribe,
				SkillName.Lockpicking,
				SkillName.Magery,
				SkillName.MagicResist,
				SkillName.Tactics,
				SkillName.Snooping,
				SkillName.Musicianship,
				SkillName.Poisoning,
				SkillName.Archery,
				SkillName.SpiritSpeak,
				SkillName.Stealing,
				SkillName.Tailoring,
				SkillName.AnimalTaming,
				SkillName.TasteID,
				SkillName.Tinkering,
				SkillName.Tracking,
				SkillName.Veterinary,
				SkillName.Swords,
				SkillName.Macing,
				SkillName.Fencing,
				SkillName.Wrestling,
				SkillName.Lumberjacking,
				SkillName.Mining,
				SkillName.Meditation,
				SkillName.Stealth,
				SkillName.RemoveTrap,
				SkillName.Necromancy,
				SkillName.Focus,
				SkillName.Chivalry,
				SkillName.Bushido,
				SkillName.Ninjitsu,
				SkillName.Spellweaving
			};

		private static SkillName[] m_CombatSkills = new SkillName[]
			{
				SkillName.Archery,
				SkillName.Swords,
				SkillName.Macing,
				SkillName.Fencing,
				SkillName.Wrestling
			};

		private static SkillName[] m_CraftSkills = new SkillName[]
			{
				SkillName.Alchemy,
				SkillName.Blacksmith,
				SkillName.Fletching,
				SkillName.Carpentry,
				SkillName.Cartography,
				SkillName.Cooking,
				SkillName.Inscribe,
				SkillName.Tailoring,
				SkillName.Tinkering
			};

		public static void FixPoints( ref Point3D top, ref Point3D bottom )
		{
			if ( bottom.m_X < top.m_X )
			{
				int swap = top.m_X;
				top.m_X = bottom.m_X;
				bottom.m_X = swap;
			}

			if ( bottom.m_Y < top.m_Y )
			{
				int swap = top.m_Y;
				top.m_Y = bottom.m_Y;
				bottom.m_Y = swap;
			}

			if ( bottom.m_Z < top.m_Z )
			{
				int swap = top.m_Z;
				top.m_Z = bottom.m_Z;
				bottom.m_Z = swap;
			}
		}

		public static bool RangeCheck( IPoint2D p1, IPoint2D p2, int range )
		{
			return p1.X >= p2.X - range
				&& p1.X <= p2.X + range
				&& p1.Y >= p2.Y - range
				&& p2.Y <= p2.Y + range;
		}

		public static void FormatBuffer( TextWriter output, Stream input, int length )
		{
			output.WriteLine( "        0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F" );
			output.WriteLine( "       -- -- -- -- -- -- -- --  -- -- -- -- -- -- -- --" );

			int byteIndex = 0;

			int whole = length >> 4;
			int rem = length & 0xF;

			for ( int i = 0; i < whole; ++i, byteIndex += 16 )
			{
				StringBuilder bytes = new StringBuilder( 49 );
				StringBuilder chars = new StringBuilder( 16 );

				for ( int j = 0; j < 16; ++j )
				{
					int c = input.ReadByte();

					bytes.Append( c.ToString( "X2" ) );

					if ( j != 7 )
					{
						bytes.Append( ' ' );
					}
					else
					{
						bytes.Append( "  " );
					}

					if ( c >= 0x20 && c < 0x7F )
					{
						chars.Append( (char)c );
					}
					else
					{
						chars.Append( '.' );
					}
				}

				output.Write( byteIndex.ToString( "X4" ) );
				output.Write( "   " );
				output.Write( bytes.ToString() );
				output.Write( "  " );
				output.WriteLine( chars.ToString() );
			}

			if ( rem != 0 )
			{
				StringBuilder bytes = new StringBuilder( 49 );
				StringBuilder chars = new StringBuilder( rem );

				for ( int j = 0; j < 16; ++j )
				{
					if ( j < rem )
					{
						int c = input.ReadByte();

						bytes.Append( c.ToString( "X2" ) );

						if ( j != 7 )
						{
							bytes.Append( ' ' );
						}
						else
						{
							bytes.Append( "  " );
						}

						if ( c >= 0x20 && c < 0x7F )
						{
							chars.Append( (char)c );
						}
						else
						{
							chars.Append( '.' );
						}
					}
					else
					{
						bytes.Append( "   " );
					}
				}

				output.Write( byteIndex.ToString( "X4" ) );
				output.Write( "   " );
				output.Write( bytes.ToString() );
				output.Write( "  " );
				output.WriteLine( chars.ToString() );
			}
		}

		private static Stack<ConsoleColor> m_ConsoleColors = new Stack<ConsoleColor>();

		public static void PushColor( ConsoleColor color )
		{
			try
			{
				m_ConsoleColors.Push( Console.ForegroundColor );
				Console.ForegroundColor = color;
			}
			catch
			{
			}
		}

		public static void PopColor()
		{
			try
			{
				Console.ForegroundColor = m_ConsoleColors.Pop();
			}
			catch
			{
			}
		}

		public static bool NumberBetween( double num, int bound1, int bound2, double allowance )
		{
			if ( bound1 > bound2 )
			{
				int i = bound1;
				bound1 = bound2;
				bound2 = i;
			}

			return num<bound2+allowance && num>bound1-allowance;
		}

		public static void AssignRandomHair( Mobile m )
		{
			AssignRandomHair( m, true );
		}

		public static void AssignRandomHair( Mobile m, int hue )
		{
			m.HairItemID = m.Race.RandomHair( m );
			m.HairHue = hue;
		}

		public static void AssignRandomHair( Mobile m, bool randomHue )
		{
			m.HairItemID = m.Race.RandomHair( m );

			if( randomHue )
				m.HairHue = m.Race.RandomHairHue();
		}

		public static void AssignRandomFacialHair( Mobile m, int hue )
		{
			m.FacialHairItemID = m.Race.RandomFacialHair( m );
			m.FacialHairHue = hue;
		}

#if MONO
		public static List<TOutput> CastConvertList<TInput, TOutput>( List<TInput> list ) where TInput : class where TOutput : class
		{
			return list.ConvertAll<TOutput>( new  Converter<TInput, TOutput>( delegate( TInput value ) { return value as TOutput; } ) );
		}
#else
		public static List<TOutput> CastConvertList<TInput, TOutput>( List<TInput> list ) where TOutput : TInput
		{
			return list.ConvertAll<TOutput>( new Converter<TInput, TOutput>( delegate( TInput value ) { return (TOutput)value; } ) );
		}
#endif
	}
}