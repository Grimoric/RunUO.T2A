namespace Server.Items
{
    public class MetalShield : BaseShield
	{
		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int ArmorBase{ get{ return 11; } }

		[Constructable]
		public MetalShield() : base( 0x1B7B )
		{
			Weight = 6.0;
		}

		public MetalShield( Serial serial ) : base(serial)
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
