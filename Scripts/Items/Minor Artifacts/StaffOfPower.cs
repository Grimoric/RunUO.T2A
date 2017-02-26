namespace Server.Items
{
    public class StaffOfPower : BlackStaff
	{
		public override int LabelNumber{ get{ return 1070692; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public StaffOfPower()
		{
			Hue = 0x4F2;
		}

		public StaffOfPower( Serial serial ) : base( serial )
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