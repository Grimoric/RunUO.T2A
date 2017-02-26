namespace Server.Items
{
    public class FangOfRactus : Kryss
	{
		public override int LabelNumber{ get{ return 1094892; } } // Fang of Ractus [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public FangOfRactus()
		{
			Hue = 0x117;
		}

		public FangOfRactus( Serial serial ) : base( serial )
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
