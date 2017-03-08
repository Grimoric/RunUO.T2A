namespace Server.Items
{
    public class RawChickenLeg : CookableFood
    {
        [Constructable]
        public RawChickenLeg() : base( 0x1607, 10 )
        {
            Weight = 1.0;
            Stackable = true;
        }

        public RawChickenLeg( Serial serial ) : base( serial )
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
            return new ChickenLeg();
        }
    }
}