namespace Server.Items
{
    public class ColdForgedBlade : ElvenSpellblade
	{
		public override int LabelNumber{ get{ return 1072916; } } // Cold Forged Blade

		[Constructable]
		public ColdForgedBlade()
		{
		}

		public ColdForgedBlade( Serial serial ) : base( serial )
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