using System;

namespace Server.Items
{
    // TODO: Flipable attributes

	[TypeAlias( "Server.Items.BottleAle", "Server.Items.BottleLiquor", "Server.Items.BottleWine" )]
	public class BeverageBottle : BaseBeverage
	{
		public override int BaseLabelNumber { get { return 1042959; } } // a bottle of Ale
		public override int MaxQuantity { get { return 5; } }
		public override bool Fillable { get { return false; } }

		public override int ComputeItemID()
		{
			if( !IsEmpty )
			{
				switch( Content )
				{
					case BeverageType.Ale: return 0x99F;
					case BeverageType.Cider: return 0x99F;
					case BeverageType.Liquor: return 0x99B;
					case BeverageType.Milk: return 0x99B;
					case BeverageType.Wine: return 0x9C7;
					case BeverageType.Water: return 0x99B;
				}
			}

			return 0;
		}

		[Constructable]
		public BeverageBottle( BeverageType type )
			: base( type )
		{
			Weight = 1.0;
		}

		public BeverageBottle( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
					{
						if( CheckType( "BottleAle" ) )
						{
							Quantity = MaxQuantity;
							Content = BeverageType.Ale;
						}
						else if( CheckType( "BottleLiquor" ) )
						{
							Quantity = MaxQuantity;
							Content = BeverageType.Liquor;
						}
						else if( CheckType( "BottleWine" ) )
						{
							Quantity = MaxQuantity;
							Content = BeverageType.Wine;
						}
						else
						{
							throw new Exception( World.LoadingType );
						}

						break;
					}
			}
		}
	}
}