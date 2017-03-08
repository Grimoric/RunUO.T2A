namespace Server.Items
{
    public class CeramicMug : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1042982; } } // a ceramic mug of Ale
        public override int MaxQuantity { get { return 1; } }

        public override int ComputeItemID()
        {
            if( ItemID >= 0x995 && ItemID <= 0x999 )
                return ItemID;
            else if( ItemID == 0x9CA )
                return ItemID;

            return 0x995;
        }

        [Constructable]
        public CeramicMug()
        {
            Weight = 1.0;
        }

        [Constructable]
        public CeramicMug( BeverageType type )
            : base( type )
        {
            Weight = 1.0;
        }

        public CeramicMug( Serial serial )
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