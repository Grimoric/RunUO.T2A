namespace Server.Items
{
	public class Wheat : Food
	{
		[Constructable]
		public Wheat() : this( 1 )
		{
		}

		[Constructable]
		public Wheat( int amount ) : base( amount, 0x1EBD)
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Wheat( Serial serial ) : base( serial )
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