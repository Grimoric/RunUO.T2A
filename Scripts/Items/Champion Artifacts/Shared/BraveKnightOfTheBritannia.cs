namespace Server.Items
{
    public class BraveKnightOfTheBritannia : Katana
	{
		public override int LabelNumber{ get{ return 1094909; } } // Brave Knight of The Britannia [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public BraveKnightOfTheBritannia()
		{
			Hue = 0x47e;
		}

		public BraveKnightOfTheBritannia( Serial serial ) : base( serial )
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
