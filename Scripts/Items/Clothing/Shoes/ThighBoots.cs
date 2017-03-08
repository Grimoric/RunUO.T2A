namespace Server.Items
{
    [Flipable]
    public class ThighBoots : BaseShoes
    {
        public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

        [Constructable]
        public ThighBoots() : this( 0 )
        {
        }

        [Constructable]
        public ThighBoots( int hue ) : base( 0x1711, hue )
        {
            Weight = 4.0;
        }

        public ThighBoots( Serial serial ) : base( serial )
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