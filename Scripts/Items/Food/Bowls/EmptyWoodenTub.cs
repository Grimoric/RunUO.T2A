namespace Server.Items
{
    [TypeAlias( "EmptyLargeWoodenBowl" )]
    public class EmptyWoodenTub : Item
    {
        [Constructable]
        public EmptyWoodenTub() : base( 0x1605 )
        {
            Weight = 2.0;
        }

        public EmptyWoodenTub( Serial serial ) : base( serial )
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