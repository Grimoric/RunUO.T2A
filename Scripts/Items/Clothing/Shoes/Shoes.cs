namespace Server.Items
{
    [FlipableAttribute( 0x170f, 0x1710 )]
	public class Shoes : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public Shoes() : this( 0 )
		{
		}

		[Constructable]
		public Shoes( int hue ) : base( 0x170F, hue )
		{
			Weight = 2.0;
		}

		public Shoes( Serial serial ) : base( serial )
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
