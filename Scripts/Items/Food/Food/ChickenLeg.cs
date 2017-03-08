namespace Server.Items
{
    public class ChickenLeg : Food
    {
        [Constructable]
        public ChickenLeg() : this( 1 )
        {
        }

        [Constructable]
        public ChickenLeg( int amount ) : base( amount, 0x1608 )
        {
            this.Weight = 1.0;
            this.FillFactor = 4;
        }

        public ChickenLeg( Serial serial ) : base( serial )
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