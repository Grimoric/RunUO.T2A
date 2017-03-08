namespace Server.Items
{
    public class PewterMug : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1042994; } } // a pewter mug with Ale
        public override int MaxQuantity { get { return 1; } }

        public override int ComputeItemID()
        {
            if( ItemID >= 0xFFF && ItemID <= 0x1002 )
                return ItemID;

            return 0xFFF;
        }

        [Constructable]
        public PewterMug()
        {
            Weight = 1.0;
        }

        [Constructable]
        public PewterMug( BeverageType type )
            : base( type )
        {
            Weight = 1.0;
        }

        public PewterMug( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int)1 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}