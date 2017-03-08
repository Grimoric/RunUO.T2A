namespace Server.Items
{
    public class WoodenBowl : Item
    {
        [Constructable]
        public WoodenBowl() : base( 0x15f8 )
        {
            Weight = 1.0;
        }

        public WoodenBowl( Serial serial ) : base( serial )
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