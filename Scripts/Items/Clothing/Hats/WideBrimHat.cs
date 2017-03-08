namespace Server.Items
{
    public class WideBrimHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        [Constructable]
        public WideBrimHat() : this( 0 )
        {
        }

        [Constructable]
        public WideBrimHat( int hue ) : base( 0x1714, hue )
        {
            Weight = 1.0;
        }

        public WideBrimHat( Serial serial ) : base( serial )
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