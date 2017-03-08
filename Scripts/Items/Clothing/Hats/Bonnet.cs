namespace Server.Items
{
    public class Bonnet : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        [Constructable]
        public Bonnet() : this( 0 )
        {
        }

        [Constructable]
        public Bonnet( int hue ) : base( 0x1719, hue )
        {
            Weight = 1.0;
        }

        public Bonnet( Serial serial ) : base( serial )
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