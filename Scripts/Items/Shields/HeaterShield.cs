namespace Server.Items
{
    public class HeaterShield : BaseShield
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int ArmorBase{ get{ return 23; } }

		[Constructable]
		public HeaterShield() : base( 0x1B76 )
		{
			Weight = 8.0;
		}

		public HeaterShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
