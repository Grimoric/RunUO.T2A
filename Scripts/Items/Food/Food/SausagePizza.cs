namespace Server.Items
{
    public class SausagePizza : Food
    {
        public override int LabelNumber{ get{ return 1044517; } } // sausage pizza

        [Constructable]
        public SausagePizza() : base( 0x1040 )
        {
            Stackable = false;
            this.Weight = 1.0;
            this.FillFactor = 6;
        }

        public SausagePizza( Serial serial ) : base( serial )
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