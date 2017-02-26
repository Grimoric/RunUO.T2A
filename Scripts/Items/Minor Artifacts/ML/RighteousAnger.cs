namespace Server.Items
{
    public class RighteousAnger : ElvenMachete
	{
		public override int LabelNumber{ get{ return 1075049; } } // Righteous Anger

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public RighteousAnger()
		{
			Hue = 0x284;
		}

		public RighteousAnger( Serial serial ) : base( serial )
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