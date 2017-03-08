namespace Server.Items
{
    public class SlabOfBacon : Food
    {
        [Constructable]
        public SlabOfBacon() : this( 1 )
        {
        }

        [Constructable]
        public SlabOfBacon( int amount ) : base( amount, 0x976 )
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
        }

        public SlabOfBacon( Serial serial ) : base( serial )
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