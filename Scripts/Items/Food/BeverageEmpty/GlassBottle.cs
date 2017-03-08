namespace Server.Items
{
    public class GlassBottle : Item
    {
        [Constructable]
        public GlassBottle() : base( 0xe2b )
        {
            this.Weight = 0.3;
        }

        public GlassBottle( Serial serial ) : base( serial )
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