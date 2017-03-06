using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
    public class ContractOfEmployment : Item
	{
		public override int LabelNumber{ get{ return 1041243; } } // a contract of employment

		[Constructable]
		public ContractOfEmployment() : base( 0x14F0 )
		{
			Weight = 1.0;
			//LootType = LootType.Blessed;
		}

		public ContractOfEmployment( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.SendLocalizedMessage( 503248 ); // Your godly powers allow you to place this vendor whereever you wish.

				Mobile v = new PlayerVendor( from, BaseHouse.FindHouseAt( from ) );

				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );

				v.SayTo( from, 503246 ); // Ah! it feels good to be working again.

				this.Delete();
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if ( house == null )
				{
					from.SendLocalizedMessage( 503240 ); // Vendors can only be placed in houses.	
				}
				else if ( !house.IsFriend( from ) )
				{
					from.SendLocalizedMessage( 503242 ); // You must ask the owner of this building to name you a friend of the household in order to place a vendor here.
				}
				else if ( !house.Public ) 
				{
					from.SendLocalizedMessage( 503241 ); // You cannot place this vendor or barkeep.  Make sure the house is public and has sufficient storage available.
				}
				else
				{
                    bool vendor = BaseHouse.IsThereVendor(from.Location, from.Map);

                    if ( vendor )
					{
						from.SendLocalizedMessage( 1062677 ); // You cannot place a vendor or barkeep at this location.
					}
					else
					{
						Mobile v = new PlayerVendor( from, house );

						v.Direction = from.Direction & Direction.Mask;
						v.MoveToWorld( from.Location, from.Map );

						v.SayTo( from, 503246 ); // Ah! it feels good to be working again.

						this.Delete();
					}
				}
			}
		}
	}
}