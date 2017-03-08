namespace Server.Items
{
    [Flipable( 0x153d, 0x153e )]
    public class FullApron : BaseMiddleTorso
    {
        [Constructable]
        public FullApron() : this( 0 )
        {
        }

        [Constructable]
        public FullApron( int hue ) : base( 0x153d, hue )
        {
            Weight = 4.0;
        }

        public FullApron( Serial serial ) : base( serial )
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