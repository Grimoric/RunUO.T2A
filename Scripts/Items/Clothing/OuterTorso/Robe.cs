namespace Server.Items
{
    [Flipable]
    public class Robe : BaseOuterTorso
    {
        [Constructable]
        public Robe() : this( 0 )
        {
        }

        [Constructable]
        public Robe( int hue ) : base( 0x1F03, hue )
        {
            Weight = 3.0;
        }

        public Robe( Serial serial ) : base( serial )
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