namespace Server.Items
{
    public class DarkenedSky : Kama
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber { get { return 1070966; } } // Darkened Sky

		[Constructable]
		public DarkenedSky() : base()
		{
		}

		public DarkenedSky( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

	public class KasaOfTheRajin : Kasa
	{
		public override int LabelNumber { get { return 1070969; } } // Kasa of the Raj-in

		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		[Constructable]
		public KasaOfTheRajin() : base()
		{
		}

		public KasaOfTheRajin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version <= 1 )
			{
				MaxHitPoints = 255;
				HitPoints = 255;
			}

			if( version == 0 )
				LootType = LootType.Regular;
		}

	}

	public class RuneBeetleCarapace : PlateDo
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber{ get{ return 1070968; } } // Rune Beetle Carapace

		[Constructable]
		public RuneBeetleCarapace() : base()
		{
		}

		public RuneBeetleCarapace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

	public class Stormgrip : LeatherNinjaMitts
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber{ get{ return 1070970; } } // Stormgrip

		[Constructable]
		public Stormgrip() : base()
		{
		}

		public Stormgrip( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

	public class SwordOfTheStampede : NoDachi
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber { get { return 1070964; } } // Sword of the Stampede

		[Constructable]
		public SwordOfTheStampede() : base()
		{
		}

		public SwordOfTheStampede( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

	public class SwordsOfProsperity : Daisho
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber { get { return 1070963; } } // Swords of Prosperity

		[Constructable]
		public SwordsOfProsperity() : base()
		{
		}

		public SwordsOfProsperity( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

	public class TheHorselord : Yumi
	{
		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }

		public override int LabelNumber { get { return 1070967; } } // The Horselord

		[Constructable]
		public TheHorselord() : base()
		{
			Slayer = SlayerName.ElementalBan;
			Slayer2 = SlayerName.ReptilianDeath;
		}

		public TheHorselord( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TomeOfLostKnowledge : Spellbook
	{
		public override int LabelNumber { get { return 1070971; } } // Tome of Lost Knowledge

		[Constructable]
		public TomeOfLostKnowledge() : base()
		{
			LootType = LootType.Regular;
			Hue = 0x530;
		}

		public TomeOfLostKnowledge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class WindsEdge : Tessen
	{
		public override int LabelNumber { get { return 1070965; } } // Wind's Edge

		[Constructable]
		public WindsEdge() : base()
		{
		}

		public WindsEdge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override int InitMinHits { get { return 255; } }
		public override int InitMaxHits { get { return 255; } }
	}

	public enum PigmentType
	{
		None,
		ParagonGold,
		VioletCouragePurple,
		InvulnerabilityBlue,
		LunaWhite,
		DryadGreen,
		ShadowDancerBlack,
		BerserkerRed,
		NoxGreen,
		RumRed,
		FireOrange,
		FadedCoal,
		Coal,
		FadedGold,
		StormBronze,
		Rose,
		MidnightCoal,
		FadedBronze,
		FadedRose,
		DeepRose
	}

	public class PigmentsOfTokuno : BasePigmentsOfTokuno
	{
		private static int[][] m_Table = new int[][]
		{
			// Hue, Label
			new int[]{ /*PigmentType.None,*/ 0, -1 },
			new int[]{ /*PigmentType.ParagonGold,*/ 0x501, 1070987 },
			new int[]{ /*PigmentType.VioletCouragePurple,*/ 0x486, 1070988 },
			new int[]{ /*PigmentType.InvulnerabilityBlue,*/ 0x4F2, 1070989 },
			new int[]{ /*PigmentType.LunaWhite,*/ 0x47E, 1070990 },
			new int[]{ /*PigmentType.DryadGreen,*/ 0x48F, 1070991 },
			new int[]{ /*PigmentType.ShadowDancerBlack,*/ 0x455, 1070992 },
			new int[]{ /*PigmentType.BerserkerRed,*/ 0x21, 1070993 },
			new int[]{ /*PigmentType.NoxGreen,*/ 0x58C, 1070994 },
			new int[]{ /*PigmentType.RumRed,*/ 0x66C, 1070995 },
			new int[]{ /*PigmentType.FireOrange,*/ 0x54F, 1070996 },
			new int[]{ /*PigmentType.Fadedcoal,*/ 0x96A, 1079579 },
			new int[]{ /*PigmentType.Coal,*/ 0x96B, 1079580 },
			new int[]{ /*PigmentType.FadedGold,*/ 0x972, 1079581 },
			new int[]{ /*PigmentType.StormBronze,*/ 0x977, 1079582 },
			new int[]{ /*PigmentType.Rose,*/ 0x97C, 1079583 },
			new int[]{ /*PigmentType.MidnightCoal,*/ 0x96C, 1079584 },
			new int[]{ /*PigmentType.FadedBronze,*/ 0x975, 1079585 },
			new int[]{ /*PigmentType.FadedRose,*/ 0x97B, 1079586 },
			new int[]{ /*PigmentType.DeepRose,*/ 0x97E, 1079587 }
		};
		
		public static int[] GetInfo( PigmentType type )
		{
			int v = (int)type;

			if( v < 0 || v >= m_Table.Length )
				v = 0;
			
			return m_Table[v];
		}

		private PigmentType m_Type;

		[CommandProperty( AccessLevel.GameMaster )]
		public PigmentType Type
		{
			get { return m_Type; }
			set
			{
				m_Type = value;
				
				int v = (int)m_Type;

				if ( v >= 0 && v < m_Table.Length )
				{
					Hue = m_Table[v][0];
					Label = m_Table[v][1];
				}
				else
				{
					Hue = 0;
					Label = -1;
				}
			}
		}
		
		public override int LabelNumber { get { return 1070933; } } // Pigments of Tokuno

		[Constructable]
		public PigmentsOfTokuno() : this( PigmentType.None, 10 )
		{
		}

		[Constructable]
		public PigmentsOfTokuno( PigmentType type ) : this( type, type == PigmentType.None||type >= PigmentType.FadedCoal? 10 : 50 )
		{
		}

		[Constructable]
		public PigmentsOfTokuno( PigmentType type, int uses ) : base( uses )
		{
			Weight = 1.0;
			Type = type;
		}

		public PigmentsOfTokuno( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 );

			writer.WriteEncodedInt( (int)m_Type );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = InheritsItem ? 0 : reader.ReadInt(); // Required for BasePigmentsOfTokuno insertion
			
			switch ( version )
			{
				case 1: Type = (PigmentType)reader.ReadEncodedInt(); break;
				case 0: break;
			}
		}
	}
}