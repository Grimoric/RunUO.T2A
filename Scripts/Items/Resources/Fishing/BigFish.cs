using System;

namespace Server.Items
{
    public class BigFish : Item, ICarvable
	{
		private Mobile m_Fisher;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Fisher
		{
			get{ return m_Fisher; }
			set{ m_Fisher = value; }
		}

		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), Math.Max( 16, (int)Weight ) / 4 , false );
		}

		public override int LabelNumber{ get{ return 1041112; } } // a big fish

		[Constructable]
		public BigFish() : base( 0x09CC )
		{
			Weight = Utility.RandomMinMax( 3, 200 );	//TODO: Find correct formula.  max on OSI currently 200, OSI dev says it's not 200 as max, and ~ 1/1,000,000 chance to get highest
			Hue = Utility.RandomBool() ? 0x847 : 0x58C;
		}

		public BigFish( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (Mobile) m_Fisher );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Fisher = reader.ReadMobile();
					break;
				}
				case 0:
				{
					Weight = Utility.RandomMinMax( 3, 200 );
					break;
				}
			}
		}
	}
}