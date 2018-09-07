using Server.Network;

namespace Server.Items
{
    public class FoldedCloth : Item, IScissorable, IDyable
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public FoldedCloth() : this( 1 )
		{
		}

		[Constructable]
		public FoldedCloth( int amount ) : base( 0x1761 )
		{
			Stackable = true;
			Amount = amount;
		}

		public FoldedCloth( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
	}
}