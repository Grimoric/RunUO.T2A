namespace Server.Items
{
    public class SavageMask : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        public static int GetRandomHue()
        {
            int v = Utility.RandomBirdHue();

            if ( v == 2101 )
                v = 0;

            return v;
        }

        public override bool Dye( Mobile from, DyeTub sender )
        {
            from.SendLocalizedMessage( sender.FailMessage );
            return false;
        }

        [Constructable]
        public SavageMask() : this( GetRandomHue() )
        {
        }

        [Constructable]
        public SavageMask( int hue ) : base( 0x154B, hue )
        {
            Weight = 2.0;
        }

        public SavageMask( Serial serial ) : base( serial )
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

            /*if ( Hue != 0 && (Hue < 2101 || Hue > 2130) )
				Hue = GetRandomHue();*/
        }
    }
}