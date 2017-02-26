namespace Server.Items
{
    public class ResilientBracer : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1072933; } } // Resillient Bracer

		[Constructable]
		public ResilientBracer()
		{
			Hue = 0x488;
		}

		public ResilientBracer( Serial serial ) : base( serial )
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
