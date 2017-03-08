namespace Server.Items
{
    public class OrcishKinMask : BaseHat
    {
        public override int InitMinHits{ get{ return 20; } }
        public override int InitMaxHits{ get{ return 30; } }

        public override bool Dye( Mobile from, DyeTub sender )
        {
            from.SendLocalizedMessage( sender.FailMessage );
            return false;
        }

        public override string DefaultName
        {
            get { return "a mask of orcish kin"; }
        }

        [Constructable]
        public OrcishKinMask() : this( 0x8A4 )
        {
        }

        [Constructable]
        public OrcishKinMask( int hue ) : base( 0x141B, hue )
        {
            Weight = 2.0;
        }

        public override bool CanEquip( Mobile m )
        {
            if ( !base.CanEquip( m ) )
                return false;

            if ( m.BodyMod == 183 || m.BodyMod == 184 )
            {
                m.SendLocalizedMessage( 1061629 ); // You can't do that while wearing savage kin paint.
                return false;
            }

            return true;
        }

        public override void OnAdded( object parent )
        {
            base.OnAdded( parent );

            if ( parent is Mobile )
                Misc.Titles.AwardKarma( (Mobile)parent, -20, true );
        }

        public OrcishKinMask( Serial serial ) : base( serial )
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

            /*if ( Hue != 0x8A4 )
				Hue = 0x8A4;*/
        }
    }
}