namespace Server.Items
{
    public class GlovesOfThePugilist : LeatherGloves
	{
		public override int LabelNumber{ get{ return 1070690; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public GlovesOfThePugilist()
		{
			Hue = 0x6D1;
		}

		public GlovesOfThePugilist( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}