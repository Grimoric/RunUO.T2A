namespace Server.Items
{
    [Flipable( 0x1F00, 0x1EFF )]
    public class FancyDress : BaseOuterTorso
    {
        [Constructable]
        public FancyDress() : this( 0 )
        {
        }

        [Constructable]
        public FancyDress( int hue ) : base( 0x1F00, hue )
        {
            Weight = 3.0;
        }

        public FancyDress( Serial serial ) : base( serial )
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