namespace Server.Items
{
    public class QuiverOfIce : ElvenQuiver
	{
		public override int LabelNumber{ get{ return 1073110; } } // quiver of ice
	
		[Constructable]
		public QuiverOfIce() : base()
		{
			Hue = 0x4ED;
		}

		public QuiverOfIce( Serial serial ) : base( serial )
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
