namespace Server.Items
{
    public class Watermelon : Food
    {
        [Constructable]
        public Watermelon() : this( 1 )
        {
        }

        [Constructable]
        public Watermelon( int amount ) : base( amount, 0xC5C )
        {
            this.Weight = 5.0;
            this.FillFactor = 5;
        }

        public Watermelon( Serial serial ) : base( serial )
        {
        }
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 1 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            if ( version < 1 )
            {
                if ( FillFactor == 2 )
                    FillFactor = 5;

                if ( Weight == 2.0 )
                    Weight = 5.0;
            }
        }
    }
}