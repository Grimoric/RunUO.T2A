namespace Server.Items
{
    [FlipableAttribute( 0xDF1, 0xDF0 )]
	public class BlackStaff : BaseStaff
	{
		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public BlackStaff() : base( 0xDF0 )
		{
			Weight = 6.0;
		}

		public BlackStaff( Serial serial ) : base( serial )
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