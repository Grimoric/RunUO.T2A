namespace Server.Items
{
    public class CrownOfTalKeesh : Bandana
	{
		public override int LabelNumber{ get{ return 1094903; } } // Crown of Tal'Keesh [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public CrownOfTalKeesh()
		{
			Hue = 0x4F2;
		}

		public CrownOfTalKeesh( Serial serial ) : base( serial )
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
