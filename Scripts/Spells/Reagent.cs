using System;
using Server.Items;

namespace Server.Spells
{
	public class Reagent
	{
		private static Type[] m_Types = new Type[8]
			{
				typeof( BlackPearl ),
				typeof( Bloodmoss ),
				typeof( Garlic ),
				typeof( Ginseng ),
				typeof( MandrakeRoot ),
				typeof( Nightshade ),
				typeof( SulfurousAsh ),
				typeof( SpidersSilk )
			};

		public Type[] Types
		{
			get{ return m_Types; }
		}

		public static Type BlackPearl
		{
			get{ return m_Types[0]; }
			set{ m_Types[0] = value; }
		}

		public static Type Bloodmoss
		{
			get{ return m_Types[1]; }
			set{ m_Types[1] = value; }
		}

		public static Type Garlic
		{
			get{ return m_Types[2]; }
			set{ m_Types[2] = value; }
		}

		public static Type Ginseng
		{
			get{ return m_Types[3]; }
			set{ m_Types[3] = value; }
		}

		public static Type MandrakeRoot
		{
			get{ return m_Types[4]; }
			set{ m_Types[4] = value; }
		}

		public static Type Nightshade
		{
			get{ return m_Types[5]; }
			set{ m_Types[5] = value; }
		}

		public static Type SulfurousAsh
		{
			get{ return m_Types[6]; }
			set{ m_Types[6] = value; }
		}

		public static Type SpidersSilk
		{
			get{ return m_Types[7]; }
			set{ m_Types[7] = value; }
		}
	}
}