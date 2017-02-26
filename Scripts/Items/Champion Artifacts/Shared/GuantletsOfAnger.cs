namespace Server.Items
{
    public class GuantletsOfAnger : PlateGloves
	{
		public override int LabelNumber{ get{ return 1094902; } } // Gauntlets of Anger [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return false; } }

		[Constructable]
		public GuantletsOfAnger()
		{
			Hue = 0x29b;
		}

		public GuantletsOfAnger( Serial serial ) : base( serial )
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
