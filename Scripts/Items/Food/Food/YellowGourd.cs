namespace Server.Items
{
    [Flipable( 0xC64, 0xC65 )]
    public class YellowGourd : Food
    {
        [Constructable]
        public YellowGourd() : this( 1 )
        {
        }

        [Constructable]
        public YellowGourd( int amount ) : base( amount, 0xC64 )
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public YellowGourd( Serial serial ) : base( serial )
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