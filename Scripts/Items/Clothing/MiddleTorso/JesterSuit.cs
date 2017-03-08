namespace Server.Items
{
    [Flipable( 0x1f9f, 0x1fa0 )]
    public class JesterSuit : BaseMiddleTorso
    {
        [Constructable]
        public JesterSuit() : this( 0 )
        {
        }

        [Constructable]
        public JesterSuit( int hue ) : base( 0x1F9F, hue )
        {
            Weight = 4.0;
        }

        public JesterSuit( Serial serial ) : base( serial )
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