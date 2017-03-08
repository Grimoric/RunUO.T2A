namespace Server.Items
{
    [TypeAlias( "Server.Items.Pizza" )]
    public class CheesePizza : Food
    {
        public override int LabelNumber{ get{ return 1044516; } } // cheese pizza

        [Constructable]
        public CheesePizza() : base( 0x1040 )
        {
            Stackable = false;
            this.Weight = 1.0;
            this.FillFactor = 6;
        }

        public CheesePizza( Serial serial ) : base( serial )
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