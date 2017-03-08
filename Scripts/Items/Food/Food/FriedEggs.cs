namespace Server.Items
{
    public class FriedEggs : Food
    {
        [Constructable]
        public FriedEggs() : this( 1 )
        {
        }

        [Constructable]
        public FriedEggs( int amount ) : base( amount, 0x9B6 )
        {
            this.Weight = 1.0;
            this.FillFactor = 4;
        }

        public FriedEggs( Serial serial ) : base( serial )
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