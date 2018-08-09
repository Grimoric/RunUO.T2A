using System;
using Server.Mobiles;

namespace Server.Items
{
    public class SlayerGroup
	{
		private static SlayerEntry[] m_TotalEntries;
		private static SlayerGroup[] m_Groups;

		public static SlayerEntry[] TotalEntries
		{
			get{ return m_TotalEntries; }
		}

		public static SlayerGroup[] Groups
		{
			get{ return m_Groups; }
		}

		public static SlayerEntry GetEntryByName( SlayerName name )
		{
			int v = (int)name;

			if ( v >= 0 && v < m_TotalEntries.Length )
				return m_TotalEntries[v];

			return null;
		}

		public static SlayerName GetLootSlayerType( Type type )
		{
			for ( int i = 0; i < m_Groups.Length; ++i )
			{
				SlayerGroup group = m_Groups[i];
				Type[] foundOn = group.FoundOn;

				bool inGroup = false;

				for ( int j = 0; foundOn != null && !inGroup && j < foundOn.Length; ++j )
					inGroup = foundOn[j] == type;

				if ( inGroup )
				{
					int index = Utility.Random( 1 + group.Entries.Length );

					if ( index == 0 )
						return group.m_Super.Name;

					return group.Entries[index - 1].Name;
				}
			}

			return SlayerName.Silver;
		}

		static SlayerGroup()
		{
			SlayerGroup humanoid = new SlayerGroup();
			SlayerGroup undead = new SlayerGroup();
			SlayerGroup elemental = new SlayerGroup();
			SlayerGroup abyss = new SlayerGroup();
			SlayerGroup arachnid = new SlayerGroup();
			SlayerGroup reptilian = new SlayerGroup();
			SlayerGroup fey = new SlayerGroup();

			humanoid.Opposition = new SlayerGroup[]{ undead };
			humanoid.FoundOn = new Type[]{ typeof( BoneKnight ), typeof( Lich ), typeof( LichLord ) };
			humanoid.Super = new SlayerEntry( SlayerName.Repond, typeof( Cyclops ), typeof( Ettin ), typeof( EvilMage ), typeof( EvilMageLord ), typeof( FrostTroll ), typeof( Ogre ), typeof( OgreLord ), typeof( Orc ), typeof( OrcCaptain ), typeof( OrcishLord ), typeof( OrcishMage ), typeof( Ratman ), typeof( RatmanArcher ), typeof( RatmanMage ), typeof( Titan ), typeof( Troll ) );
			humanoid.Entries = new SlayerEntry[]
				{
					new SlayerEntry( SlayerName.OgreTrashing, typeof( Ogre ), typeof( OgreLord ) ),
					new SlayerEntry( SlayerName.OrcSlaying, typeof( Orc ), typeof( OrcCaptain ), typeof( OrcishLord ), typeof( OrcishMage ) ),
					new SlayerEntry( SlayerName.TrollSlaughter, typeof( Troll ), typeof( FrostTroll ) )
				};

			undead.Opposition = new SlayerGroup[]{ humanoid };
			undead.Super = new SlayerEntry( SlayerName.Silver, typeof( BoneKnight ), typeof( BoneMagi ), typeof( Ghoul ), typeof( Lich ), typeof( LichLord ), typeof( Mummy ), typeof( RottingCorpse ), typeof( Shade ), typeof( SkeletalKnight ), typeof( SkeletalMage ), typeof( Skeleton ), typeof( Spectre ), typeof( Wraith ), typeof( Zombie ) );
			undead.Entries = new SlayerEntry[0];

			fey.Opposition = new SlayerGroup[]{ abyss };
			fey.Super = new SlayerEntry( SlayerName.Fey, typeof( Wisp ) );
			fey.Entries = new SlayerEntry[0];

			elemental.Opposition = new SlayerGroup[]{ abyss };
			elemental.FoundOn = new Type[]{ typeof( Balron ), typeof( Daemon ) };
			elemental.Super = new SlayerEntry( SlayerName.ElementalBan, typeof( AirElemental ), typeof( BloodElemental ), typeof( EarthElemental ), typeof( Efreet ), typeof( FireElemental ), typeof( IceElemental ), typeof( PoisonElemental ), typeof( SnowElemental ), typeof( WaterElemental ) );
			elemental.Entries = new SlayerEntry[]
				{
					new SlayerEntry( SlayerName.BloodDrinking, typeof( BloodElemental ) ),
					new SlayerEntry( SlayerName.EarthShatter, typeof( EarthElemental ) ),
					new SlayerEntry( SlayerName.ElementalHealth, typeof( PoisonElemental ) ),
					new SlayerEntry( SlayerName.FlameDousing, typeof( FireElemental ) ),
					new SlayerEntry( SlayerName.SummerWind, typeof( SnowElemental ), typeof( IceElemental ) ),
					new SlayerEntry( SlayerName.Vacuum, typeof( AirElemental ) ),
					new SlayerEntry( SlayerName.WaterDissipation, typeof( WaterElemental ) )
				};

			abyss.Opposition = new SlayerGroup[]{ elemental, fey };
			abyss.FoundOn = new Type[]{ typeof( BloodElemental ) };

			abyss.Super = new SlayerEntry( SlayerName.Exorcism, typeof( Balron ), typeof( Daemon ), typeof( Gargoyle ), typeof( IceFiend ), typeof( Imp ), typeof( StoneGargoyle ) );

            abyss.Entries = new SlayerEntry[]
				{
					new SlayerEntry( SlayerName.DaemonDismissal, typeof( Balron ), typeof( Daemon ), typeof( IceFiend ), typeof( Imp ) ),
					new SlayerEntry( SlayerName.GargoylesFoe, typeof( Gargoyle ), typeof( StoneGargoyle ) ),
					new SlayerEntry( SlayerName.BalronDamnation, typeof( Balron ) )
				};

			arachnid.Opposition = new SlayerGroup[]{ reptilian };
			arachnid.FoundOn = new Type[]{ typeof( AncientWyrm ), typeof( Dragon ), typeof( OphidianMatriarch ) };
			arachnid.Super = new SlayerEntry( SlayerName.ArachnidDoom, typeof( FrostSpider ), typeof( GiantSpider ), typeof( Scorpion ), typeof( TerathanAvenger ), typeof( TerathanDrone ), typeof( TerathanMatriarch ), typeof( TerathanWarrior ) );
			arachnid.Entries = new SlayerEntry[]
				{
					new SlayerEntry( SlayerName.ScorpionsBane, typeof( Scorpion ) ),
					new SlayerEntry( SlayerName.SpidersDeath, typeof( FrostSpider ), typeof( GiantSpider ) ),
					new SlayerEntry( SlayerName.Terathan, typeof( TerathanAvenger ), typeof( TerathanDrone ), typeof( TerathanMatriarch ), typeof( TerathanWarrior ) )
				};

			reptilian.Opposition = new SlayerGroup[]{ arachnid };
			reptilian.FoundOn = new Type[]{ typeof( TerathanAvenger ), typeof( TerathanMatriarch ) };
			reptilian.Super = new SlayerEntry( SlayerName.ReptilianDeath, typeof( AncientWyrm ), typeof( DeepSeaSerpent ), typeof( Dragon ), typeof( Drake ), typeof( IceSerpent ), typeof( GiantSerpent ), typeof( IceSnake ), typeof( LavaSerpent ), typeof( LavaSnake ), typeof( Lizardman ), typeof( OphidianArchmage ), typeof( OphidianKnight ), typeof( OphidianMage ), typeof( OphidianMatriarch ), typeof( OphidianWarrior ), typeof( SeaSerpent ), typeof( SilverSerpent ), typeof( Snake ), typeof( WhiteWyrm ), typeof( Wyvern ) );
			reptilian.Entries = new SlayerEntry[]
				{
					new SlayerEntry( SlayerName.DragonSlaying, typeof( AncientWyrm ), typeof( Dragon ), typeof( Drake ), typeof( WhiteWyrm ), typeof( Wyvern ) ),
					new SlayerEntry( SlayerName.LizardmanSlaughter, typeof( Lizardman ) ),
					new SlayerEntry( SlayerName.Ophidian, typeof( OphidianArchmage ), typeof( OphidianKnight ), typeof( OphidianMage ), typeof( OphidianMatriarch ), typeof( OphidianWarrior ) ),
					new SlayerEntry( SlayerName.SnakesBane, typeof( DeepSeaSerpent ), typeof( GiantSerpent ), typeof( IceSerpent ), typeof( IceSnake ), typeof( LavaSerpent ), typeof( LavaSnake ), typeof( SeaSerpent ), typeof( SilverSerpent ), typeof( Snake ) )
				};

			m_Groups = new SlayerGroup[]
				{
					humanoid,
					undead,
					elemental,
					abyss,
					arachnid,
					reptilian,
					fey
				};

			m_TotalEntries = CompileEntries( m_Groups );
		}

		private static SlayerEntry[] CompileEntries( SlayerGroup[] groups )
		{
			SlayerEntry[] entries = new SlayerEntry[28];

			for ( int i = 0; i < groups.Length; ++i )
			{
				SlayerGroup g = groups[i];

				g.Super.Group = g;

				entries[(int)g.Super.Name] = g.Super;

				for ( int j = 0; j < g.Entries.Length; ++j )
				{
					g.Entries[j].Group = g;
					entries[(int)g.Entries[j].Name] = g.Entries[j];
				}
			}

			return entries;
		}

		private SlayerGroup[] m_Opposition;
		private SlayerEntry m_Super;
		private SlayerEntry[] m_Entries;
		private Type[] m_FoundOn;

		public SlayerGroup[] Opposition{ get{ return m_Opposition; } set{ m_Opposition = value; } }
		public SlayerEntry Super{ get{ return m_Super; } set{ m_Super = value; } }
		public SlayerEntry[] Entries{ get{ return m_Entries; } set{ m_Entries = value; } }
		public Type[] FoundOn{ get{ return m_FoundOn; } set{ m_FoundOn = value; } }

		public bool OppositionSuperSlays( Mobile m )
		{
			for( int i = 0; i < Opposition.Length; i++ )
			{
				if ( Opposition[i].Super.Slays( m ) )
					return true;
			}

			return false;
		}

		public SlayerGroup()
		{
		}
	}
}