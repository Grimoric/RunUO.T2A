namespace Server.Items
{
    public class IronwoodCrown : RavenHelm
	{
		public override int LabelNumber{ get{ return 1072924; } } // Ironwood Crown

		[Constructable]
		public IronwoodCrown()
		{
			Hue = 0x1;
		}

		public IronwoodCrown( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}