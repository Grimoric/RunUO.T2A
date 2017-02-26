namespace Server.Items
{
    [FlipableAttribute( 0x26BE, 0x26C8 )]
	public class Pike : BaseSpear
	{
		public override int OldStrengthReq{ get{ return 50; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public Pike() : base( 0x26BE )
		{
			Weight = 8.0;
		}

		public Pike( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}