namespace Server.Items
{
    public class SmallPumpkin : Food
    {
        [Constructable]
        public SmallPumpkin() : this( 1 )
        {
        }

        [Constructable]
        public SmallPumpkin( int amount ) : base( amount, 0xC6C )
        {
            this.Weight = 1.0;
            this.FillFactor = 8;
        }

        public SmallPumpkin( Serial serial ) : base( serial )
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