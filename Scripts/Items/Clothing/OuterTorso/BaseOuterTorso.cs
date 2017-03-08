namespace Server.Items
{
	public abstract class BaseOuterTorso : BaseClothing
	{
		public BaseOuterTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseOuterTorso( int itemID, int hue ) : base( itemID, Layer.OuterTorso, hue )
		{
		}

		public BaseOuterTorso( Serial serial ) : base( serial )
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