namespace Server.Items
{
    public class PhantomStaff : WildStaff
	{
		public override int LabelNumber{ get{ return 1072919; } } // Phantom Staff

		[Constructable]
		public PhantomStaff()
		{
			Hue = 0x1;
		}

		public PhantomStaff( Serial serial ) : base( serial )
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