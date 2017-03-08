namespace Server.Items
{
    [Flipable( 0xc70, 0xc71 )]
    public class Lettuce : Food
    {
        [Constructable]
        public Lettuce() : this( 1 )
        {
        }

        [Constructable]
        public Lettuce( int amount ) : base( amount, 0xc70 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public Lettuce( Serial serial ) : base( serial )
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