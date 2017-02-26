namespace Server.Items
{
    public class SongWovenMantle : LeafArms
	{
		public override int LabelNumber{ get{ return 1072931; } } // Song Woven Mantle

		[Constructable]
		public SongWovenMantle()
		{
			Hue = 0x493;
		}

		public SongWovenMantle( Serial serial ) : base( serial )
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