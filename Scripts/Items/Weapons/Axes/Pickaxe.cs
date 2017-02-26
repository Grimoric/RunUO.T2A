using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute( 0xE86, 0xE85 )]
	public class Pickaxe : BaseAxe, IUsesRemaining
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public Pickaxe() : base( 0xE86 )
		{
			Weight = 11.0;
			UsesRemaining = 50;
			ShowUsesRemaining = true;
		}

		public Pickaxe( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}
}