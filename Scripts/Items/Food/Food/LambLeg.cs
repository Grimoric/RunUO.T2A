namespace Server.Items
{
    public class LambLeg : Food
    {
        [Constructable]
        public LambLeg() : this( 1 )
        {
        }

        [Constructable]
        public LambLeg( int amount ) : base( amount, 0x160a )
        {
            this.Weight = 2.0;
            this.FillFactor = 5;
        }

        public LambLeg( Serial serial ) : base( serial )
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