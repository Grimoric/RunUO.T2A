using System;
using Server.Prompts;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
    [FlipableAttribute( 0x1f14, 0x1f15, 0x1f16, 0x1f17 )]
	public class RecallRune : Item
	{
		private string m_Description;
		private bool m_Marked;
		private Point3D m_Target;
		private Map m_TargetMap;
		private BaseHouse m_House;

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			if ( m_House != null && !m_House.Deleted )
			{
				writer.Write( (int) 1 ); // version

				writer.Write( (Item) m_House );
			}
			else
			{
				writer.Write( (int) 0 ); // version
			}

			writer.Write( (string) m_Description );
			writer.Write( (bool) m_Marked );
			writer.Write( (Point3D) m_Target );
			writer.Write( (Map) m_TargetMap );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_House = reader.ReadItem() as BaseHouse;
					goto case 0;
				}
				case 0:
				{
					m_Description = reader.ReadString();
					m_Marked = reader.ReadBool();
					m_Target = reader.ReadPoint3D();
					m_TargetMap = reader.ReadMap();

					CalculateHue();

					break;
				}
			}
		}

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public BaseHouse House
		{
			get
			{
				if ( m_House != null && m_House.Deleted )
					House = null;

				return m_House;
			}
			set{ m_House = value; CalculateHue(); }
		}

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				m_Description = value;
			}
		}

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public bool Marked
		{
			get
			{
				return m_Marked;
			}
			set
			{
				if ( m_Marked != value )
				{
					m_Marked = value;
					CalculateHue();
				}
			}
		}

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public Point3D Target
		{
			get
			{
				return m_Target;
			}
			set
			{
				m_Target = value;
			}
		}

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public Map TargetMap
		{
			get
			{
				return m_TargetMap;
			}
			set
			{
				if ( m_TargetMap != value )
				{
					m_TargetMap = value;
					CalculateHue();
				}
			}
		}

		private void CalculateHue()
		{
			if ( !m_Marked )
				Hue = 0;
			else if ( m_TargetMap == Map.Felucca )
				Hue = House != null ? 0x66D : 0;
		}

		public void Mark( Mobile m )
		{
			m_Marked = true;

			m_House = null;
			m_Target = m.Location;
			m_TargetMap = m.Map;

			m_Description = BaseRegion.GetRuneNameFor( Region.Find( m_Target, m_TargetMap ) );

			CalculateHue();
		}

		private const string RuneFormat = "a recall rune for {0}";

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Marked )
			{
				string desc;

				if ( (desc = m_Description) == null || (desc = desc.Trim()).Length == 0 )
					desc = "an unknown location";

				if ( m_TargetMap == Map.Felucca )
					LabelTo( from, House != null ? 1062452 : 1060805, String.Format( RuneFormat, desc ) ); // ~1_val~ (Felucca)[(House)]
				else
					LabelTo( from, House != null ? "{0} ({1})(House)" : "{0} ({1})", String.Format( RuneFormat, desc ), m_TargetMap );
			}
			else
			{
				LabelTo( from, "an unmarked recall rune" );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			int number;

			if ( !IsChildOf( from.Backpack ) )
			{
				number = 1042001; // That must be in your pack for you to use it.
			}
			else if ( House != null )
			{
				number = 1062399; // You cannot edit the description for this rune.
			}
			else if ( m_Marked )
			{
				number = 501804; // Please enter a description for this marked object.

				from.Prompt = new RenamePrompt( this );
			}
			else
			{
				number = 501805; // That rune is not yet marked.
			}

			from.SendLocalizedMessage( number );
		}

		private class RenamePrompt : Prompt
		{
			private RecallRune m_Rune;

			public RenamePrompt( RecallRune rune )
			{
				m_Rune = rune;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( m_Rune.House == null && m_Rune.Marked )
				{
					m_Rune.Description = text;
					from.SendLocalizedMessage( 1010474 ); // The etching on the rune has been changed.
				}
			}
		}

		[Constructable]
		public RecallRune() : base( 0x1F14 )
		{
			Weight = 1.0;
			CalculateHue();
		}

		public RecallRune( Serial serial ) : base( serial )
		{
		}
	}
}