using System;
using System.Collections;
using Server.Mobiles;

namespace Server
{
    public class SpeedInfo
	{
		// Should we use the new method of speeds?
		private static bool Enabled = true;

		private double m_ActiveSpeed;
		private double m_PassiveSpeed;
		private Type[] m_Types;

		public double ActiveSpeed
		{
			get{ return m_ActiveSpeed; }
			set{ m_ActiveSpeed = value; }
		}

		public double PassiveSpeed
		{
			get{ return m_PassiveSpeed; }
			set{ m_PassiveSpeed = value; }
		}

		public Type[] Types
		{
			get{ return m_Types; }
			set{ m_Types = value; }
		}

		public SpeedInfo( double activeSpeed, double passiveSpeed, Type[] types )
		{
			m_ActiveSpeed = activeSpeed;
			m_PassiveSpeed = passiveSpeed;
			m_Types = types;
		}

		public static bool Contains( object obj )
		{
			if ( !Enabled )
				return false;

			if ( m_Table == null )
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[obj.GetType()];

			return sp != null;
		}

		public static bool GetSpeeds( object obj, ref double activeSpeed, ref double passiveSpeed )
		{
			if ( !Enabled )
				return false;

			if ( m_Table == null )
				LoadTable();

			SpeedInfo sp = (SpeedInfo)m_Table[obj.GetType()];

			if ( sp == null )
				return false;

			activeSpeed = sp.ActiveSpeed;
			passiveSpeed = sp.PassiveSpeed;

			return true;
		}

		private static void LoadTable()
		{
			m_Table = new Hashtable();

			for ( int i = 0; i < m_Speeds.Length; ++i )
			{
				SpeedInfo info = m_Speeds[i];
				Type[] types = info.Types;

				for ( int j = 0; j < types.Length; ++j )
					m_Table[types[j]] = info;
			}
		}

		private static Hashtable m_Table;

		private static SpeedInfo[] m_Speeds = new SpeedInfo[]
			{
				/* Slow */
				new SpeedInfo( 0.3, 0.6, new Type[]
				{
					typeof( BoneKnight ),		typeof( EarthElemental ),
					typeof( Ettin ),			typeof( FrostOoze ),		typeof( FrostTroll ),
					typeof( Ghoul ),
					typeof( HeadlessOne ),		typeof( Jwilson ),			typeof( Mummy ),
					typeof( Ogre ),				typeof( OgreLord ),
					typeof( Rat ),				typeof( RottingCorpse ),
					typeof( Sewerrat ),			typeof( Skeleton ),			typeof( Slime ),
					typeof( Zombie ),			typeof( Walrus )
				} ),
				/* Fast */
				new SpeedInfo( 0.2, 0.4, new Type[]
				{
					typeof( AirElemental ),
					typeof( AncientWyrm ),		typeof( Balron ),			typeof( BladeSpirits ),
                    typeof( DreadSpider ),      typeof( Efreet ),
					typeof( Lich ),				typeof( Nightmare ),		typeof( OphidianArchmage ),
					typeof( OphidianMage ),		typeof( OphidianWarrior ),	typeof( OphidianMatriarch ),
					typeof( OphidianKnight ),	typeof( PoisonElemental ),
					typeof( SnowElemental ),	typeof( WhiteWyrm ),		typeof( Wisp )
				} ),
				/* Very Fast */
				new SpeedInfo( 0.175, 0.350, new Type[]
				{
					typeof( EnergyVortex ),     typeof( SilverSerpent )
				} ),
				/* Medium */
				new SpeedInfo( 0.25, 0.5, new Type[]
				{
					typeof( Alligator ),
					typeof( Bird ),
					typeof( BlackBear ),
					typeof( BloodElemental ),	typeof( Boar ),				
					typeof( BoneMagi ),			typeof( Brigand ),			
					typeof( BrownBear ),		typeof( Bull ),				typeof( BullFrog ),
					typeof( Cat ),							
					typeof( Chicken ),			
					typeof( Cougar ),			typeof( Cow ),
					typeof( Cyclops ),			typeof( Daemon ),			typeof( DeepSeaSerpent ),
					typeof( DesertOstard ),		typeof( DireWolf ),			typeof( Dog ),
					typeof( Dolphin ),			typeof( Dragon ),			typeof( Drake ),
					typeof( Eagle ),			typeof( ElderGazer ),
					typeof( EvilMage ),			typeof( EvilMageLord ),		typeof( Executioner ),
					typeof( FireElemental ),	
					typeof( ForestOstard ),		typeof( FrenziedOstard ),
					typeof( FrostSpider ),		typeof( Gargoyle ),			typeof( Gazer ),
					typeof( IceSerpent ),		typeof( GiantRat ),			typeof( GiantSerpent ),
					typeof( GiantSpider ),		typeof( GiantToad ),		typeof( Goat ),
					typeof( Gorilla ),			typeof( GreatHart ),
					typeof( GreyWolf ),			typeof( GrizzlyBear ),
					typeof( Harpy ),			typeof( HellHound ),
					typeof( Hind ),				
					typeof( Horse ),			typeof( IceElemental ),		typeof( IceFiend ),
					typeof( IceSnake ),			typeof( Imp ),				typeof( JackRabbit ),
					typeof( Kraken ),			typeof( PredatorHellCat ),
					typeof( LavaLizard ),		typeof( LavaSerpent ),		typeof( LavaSnake ),
					typeof( Lizardman ),		typeof( Llama ),			typeof( Mongbat ),
					typeof( MountainGoat ),		typeof( Orc ),
					typeof( OrcCaptain ),
					typeof( OrcishLord ),		typeof( OrcishMage ),		typeof( PackHorse ),
					typeof( PackLlama ),		typeof( Panther ),			typeof( Pig ),
					typeof( PolarBear ),		typeof( Rabbit ),
					typeof( Ratman ),			typeof( RatmanArcher ),		typeof( RatmanMage ),
					typeof( RidableLlama ),
					typeof( Scorpion ),			typeof( SeaSerpent ),
					typeof( Shade ),			typeof( Sheep ),
					typeof( SkeletalMage ),
					typeof( HellCat ),			typeof( Snake ),
					typeof( SnowLeopard ),		typeof( Spectre ),          typeof( StrongMongbat ),
                    typeof( StoneGargoyle ),	typeof( StoneHarpy ),		
					typeof( SwampTentacle ),	typeof( TerathanAvenger ),
					typeof( TerathanDrone ),	typeof( TerathanMatriarch ), typeof( TerathanWarrior ),
					typeof( TimberWolf ),		typeof( Titan ),			typeof( Troll ),
					typeof( WaterElemental ),
                    typeof( WhiteWolf ),		typeof( Wraith ),			typeof( Wyvern ),
					typeof( LichLord ),			typeof( SkeletalKnight )
				} )
			};
	}
}