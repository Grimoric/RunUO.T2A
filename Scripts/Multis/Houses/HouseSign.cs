using System;
using Server.Gumps;

namespace Server.Multis
{
    public class HouseSign : Item
	{
		private BaseHouse m_Owner;

		public HouseSign( BaseHouse owner ) : base( 0xBD2 )
		{
			m_Owner = owner;
			Movable = false;
		}

		public HouseSign( Serial serial ) : base( serial )
		{
		}

		public string GetName()
		{
			if ( Name == null )
				return "An Unnamed House";

			return Name;
		}

		public BaseHouse Owner
		{
			get
			{
				return m_Owner;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RestrictDecay
		{
			get{ return m_Owner != null && m_Owner.RestrictDecay; }
		    set
		    {
		        if (m_Owner != null)
		        {
                    m_Owner.RestrictDecay = value;
                }
            }
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_Owner != null && !m_Owner.Deleted )
				m_Owner.Delete();
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Owner != null && BaseHouse.DecayEnabled && m_Owner.DecayPeriod != TimeSpan.Zero )
			{
				string message;

				switch ( m_Owner.DecayLevel )
				{
					case DecayLevel.Ageless:	message = "ageless"; break;
					case DecayLevel.Fairly:		message = "fairly worn"; break;
					case DecayLevel.Greatly:	message = "greatly worn"; break;
					case DecayLevel.LikeNew:	message = "like new"; break;
					case DecayLevel.Slightly:	message = "slightly worn"; break;
					case DecayLevel.Somewhat:	message = "somewhat worn"; break;
					default:					message = "in danger of collapsing"; break;
				}

				LabelTo( from, "This house is {0}.", message );
			}

			base.OnSingleClick( from );
		}

		public void ShowSign( Mobile m )
		{
			if ( m_Owner != null )
			{
				if ( m_Owner.IsFriend( m ) && m.AccessLevel < AccessLevel.GameMaster )
				{
					m_Owner.RefreshDecay();
                    m.SendLocalizedMessage( 501293 ); // Welcome back to the house, friend!
				}

				m.SendGump( new HouseGump( m, m_Owner ) );
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m_Owner == null )
				return;

			ShowSign( m );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Owner = reader.ReadItem() as BaseHouse;

					break;
				}
			}

			if ( this.Name == "a house sign" )
				this.Name = null;
		}
	}
}