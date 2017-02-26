using Server.Engines.Harvest;

namespace Server.Items
{
    public class GargoylesPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1041281; } } // a gargoyle's pickaxe
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }
		
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public GargoylesPickaxe() : this( Utility.RandomMinMax( 101, 125 ))
		{
		}

		[Constructable]
		public GargoylesPickaxe( int uses ) : base( 0xE85 + Utility.Random( 2 ))
		{
			Weight = 11.0;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public GargoylesPickaxe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if ( Hue == 0x973 )
				Hue = 0x0;
		}
	}
}