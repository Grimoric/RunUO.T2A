namespace Server.Items
{
    [Flipable]
    public class Cloak : BaseCloak
    {
        [Constructable]
        public Cloak() : this( 0 )
        {
        }

        [Constructable]
        public Cloak( int hue ) : base( 0x1515, hue )
        {
            Weight = 5.0;
        }

        public Cloak( Serial serial ) : base( serial )
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