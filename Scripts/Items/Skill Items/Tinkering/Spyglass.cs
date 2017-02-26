using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    [Flipable( 0x14F5, 0x14F6 )]
	public class Spyglass : Item
	{
		[Constructable]
		public Spyglass() : base( 0x14F5 )
		{
			Weight = 3.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1008155 ); // You peer into the heavens, seeking the moons...

			from.Send( new MessageLocalizedAffix( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 1008146 + (int)Clock.GetMoonPhase( Map.Trammel, from.X, from.Y ), "", AffixType.Prepend, "Trammel : ", "" ) );
			from.Send( new MessageLocalizedAffix( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 1008146 + (int)Clock.GetMoonPhase( Map.Felucca, from.X, from.Y ), "", AffixType.Prepend, "Felucca : ", "" ) );
		}

		public Spyglass( Serial serial ) : base( serial )
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