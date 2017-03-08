namespace Server.Items
{
    public class FrenchBread : Food
    {
        [Constructable]
        public FrenchBread() : this( 1 )
        {
        }

        [Constructable]
        public FrenchBread( int amount ) : base( amount, 0x98C )
        {
            this.Weight = 2.0;
            this.FillFactor = 3;
        }

        public FrenchBread( Serial serial ) : base( serial )
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