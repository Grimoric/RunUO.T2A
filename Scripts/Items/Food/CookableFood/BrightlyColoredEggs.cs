namespace Server.Items
{
    public class BrightlyColoredEggs : CookableFood
    {
        public override string DefaultName
        {
            get { return "brightly colored eggs"; }
        }

        [Constructable]
        public BrightlyColoredEggs() : base( 0x9B5, 15 )
        {
            Weight = 0.5;
            Hue = 3 + Utility.Random( 20 ) * 5;
        }

        public BrightlyColoredEggs( Serial serial ) : base( serial )
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
            return new FriedEggs();
        }
    }
}