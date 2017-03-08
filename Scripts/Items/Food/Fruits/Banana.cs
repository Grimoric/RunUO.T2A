namespace Server.Items
{
    [Flipable( 0x171f, 0x1720 )]
    public class Banana : Food
    {
        [Constructable]
        public Banana() : this( 1 )
        {
        }

        [Constructable]
        public Banana( int amount ) : base( amount, 0x171f )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Banana( Serial serial ) : base( serial )
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