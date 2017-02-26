using Server.Engines.Harvest;

namespace Server.Items
{
    public class SturdyPickaxe : BaseAxe, IUsesRemaining
	{
		public override int LabelNumber{ get{ return 1045126; } } // sturdy pickaxe
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public SturdyPickaxe() : this( 180 )
		{
		}

		[Constructable]
		public SturdyPickaxe( int uses ) : base( 0xE86 )
		{
			Weight = 11.0;
			Hue = 0x973;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public SturdyPickaxe( Serial serial ) : base( serial )
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
		}
	}
}