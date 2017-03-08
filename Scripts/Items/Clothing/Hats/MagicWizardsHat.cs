namespace Server.Items
{
    public class MagicWizardsHat : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        public override int LabelNumber{ get{ return 1041072; } } // a magical wizard's hat

        public override int BaseStrBonus{ get{ return -5; } }
        public override int BaseDexBonus{ get{ return -5; } }
        public override int BaseIntBonus{ get{ return +5; } }

        [Constructable]
        public MagicWizardsHat() : this( 0 )
        {
        }

        [Constructable]
        public MagicWizardsHat( int hue ) : base( 0x1718, hue )
        {
            Weight = 1.0;
        }

        public MagicWizardsHat( Serial serial ) : base( serial )
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