namespace Server.Items
{
    public class UncookedSausagePizza : CookableFood
    {
        public override int LabelNumber{ get{ return 1041337; } } // uncooked sausage pizza

        [Constructable]
        public UncookedSausagePizza() : base( 0x1083, 20 )
        {
            Weight = 1.0;
        }

        public UncookedSausagePizza( Serial serial ) : base( serial )
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

        public override Food Cook()
        {
            return new SausagePizza();
        }
    }
}