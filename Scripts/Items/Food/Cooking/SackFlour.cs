namespace Server.Items
{
    [TypeAlias( "Server.Items.SackFlourOpen" )]
    public class SackFlour : Item, IHasQuantity
    {
        private int m_Quantity;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Quantity
        {
            get{ return m_Quantity; }
            set
            {
                if ( value < 0 )
                    value = 0;
                else if ( value > 20 )
                    value = 20;

                m_Quantity = value;

                if ( m_Quantity == 0 )
                    Delete();
                else if ( m_Quantity < 20 && (ItemID == 0x1039 || ItemID == 0x1045) )
                    ++ItemID;
            }
        }

        [Constructable]
        public SackFlour() : base( 0x1039 )
        {
            Weight = 5.0;
            m_Quantity = 20;
        }

        public SackFlour( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 2 ); // version

            writer.Write( (int) m_Quantity );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
                case 2:
                case 1:
                {
                    m_Quantity = reader.ReadInt();
                    break;
                }
                case 0:
                {
                    m_Quantity = 20;
                    break;
                }
            }

            if ( version < 2 && Weight == 1.0 )
                Weight = 5.0;
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !Movable )
                return;

            if ( ItemID == 0x1039 || ItemID == 0x1045 )
                ++ItemID;

#if false
			this.Delete();

			from.AddToBackpack( new SackFlourOpen() );
#endif
        }

    }
}