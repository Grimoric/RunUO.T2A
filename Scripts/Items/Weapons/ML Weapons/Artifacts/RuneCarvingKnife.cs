namespace Server.Items
{
    public class RuneCarvingKnife : AssassinSpike
	{
		public override int LabelNumber{ get{ return 1072915; } } // Rune Carving Knife

		[Constructable]
		public RuneCarvingKnife()
		{
			Hue = 0x48D;
		}

		public RuneCarvingKnife( Serial serial ) : base( serial )
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