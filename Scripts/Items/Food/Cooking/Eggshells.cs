namespace Server.Items
{
    public class Eggshells : Item
    {
        [Constructable]
        public Eggshells() : base( 0x9b4 )
        {
            Weight = 0.5;
        }

        public Eggshells( Serial serial ) : base( serial )
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