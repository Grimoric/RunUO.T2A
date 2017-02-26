namespace Server.Items
{
    public class QuiverOfRage : BaseQuiver
	{
		public override int LabelNumber{ get{ return 1075038; } } // Quiver of Rage

		[Constructable]
		public QuiverOfRage() : base()
		{
			Hue = 0x24C;

			WeightReduction = 25;
			DamageIncrease = 10;
		}

		public QuiverOfRage( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
