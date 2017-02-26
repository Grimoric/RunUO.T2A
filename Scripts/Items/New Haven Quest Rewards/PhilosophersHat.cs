namespace Server.Items
{
    public class PhilosophersHat : WizardsHat
	{
		public override int LabelNumber{ get{ return 1077602; } } // Philosopher's Hat

		[Constructable]
		public PhilosophersHat()
		{
			LootType = LootType.Blessed;
		}

		public PhilosophersHat( Serial serial ) : base( serial )
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
