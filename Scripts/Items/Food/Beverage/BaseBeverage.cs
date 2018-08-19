using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public abstract class BaseBeverage : Item, IHasQuantity
    {
        private BeverageType m_Content;
        private int m_Quantity;
        private Mobile m_Poisoner;
        private Poison m_Poison;

        public override int LabelNumber
        {
            get
            {
                int num = BaseLabelNumber;

                if( IsEmpty || num == 0 )
                    return EmptyLabelNumber;

                return BaseLabelNumber + (int)m_Content;
            }
        }

        public virtual bool ShowQuantity { get { return MaxQuantity > 1; } }
        public virtual bool Fillable { get { return true; } }
        public virtual bool Pourable { get { return true; } }

        public virtual int EmptyLabelNumber { get { return base.LabelNumber; } }
        public virtual int BaseLabelNumber { get { return 0; } }

        public abstract int MaxQuantity { get; }

        public abstract int ComputeItemID();

        [CommandProperty( AccessLevel.GameMaster )]
        public bool IsEmpty
        {
            get { return m_Quantity <= 0; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool ContainsAlchohol
        {
            get { return !IsEmpty && m_Content != BeverageType.Milk && m_Content != BeverageType.Water; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool IsFull
        {
            get { return m_Quantity >= MaxQuantity; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Poisoner
        {
            get { return m_Poisoner; }
            set { m_Poisoner = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public BeverageType Content
        {
            get { return m_Content; }
            set
            {
                m_Content = value;

                int itemID = ComputeItemID();

                if( itemID > 0 )
                    ItemID = itemID;
                else
                    Delete();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Quantity
        {
            get { return m_Quantity; }
            set
            {
                if( value < 0 )
                    value = 0;
                else if( value > MaxQuantity )
                    value = MaxQuantity;

                m_Quantity = value;

                int itemID = ComputeItemID();

                if( itemID > 0 )
                    ItemID = itemID;
                else
                    Delete();
            }
        }

        public virtual int GetQuantityDescription()
        {
            int perc = m_Quantity * 100 / MaxQuantity;

            if( perc <= 0 )
                return 1042975; // It's empty.
            else if( perc <= 33 )
                return 1042974; // It's nearly empty.
            else if( perc <= 66 )
                return 1042973; // It's half full.
            else
                return 1042972; // It's full.
        }

        public override void OnSingleClick( Mobile from )
        {
            base.OnSingleClick( from );

            if( ShowQuantity )
                LabelTo( from, GetQuantityDescription() );
        }

        public virtual bool ValidateUse( Mobile from, bool message )
        {
            if( Deleted )
                return false;

            if( !Movable && !Fillable )
            {
                Multis.BaseHouse house = Multis.BaseHouse.FindHouseAt( this );

                if( house == null || !house.IsLockedDown( this ) )
                {
                    if( message )
                        from.SendLocalizedMessage( 502946, "", 0x59 ); // That belongs to someone else.

                    return false;
                }
            }

            if( from.Map != Map || !from.InRange( GetWorldLocation(), 2 ) || !from.InLOS( this ) )
            {
                if( message )
                    from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.

                return false;
            }

            return true;
        }

        public virtual void Fill_OnTarget( Mobile from, object targ )
        {
            if( !IsEmpty || !Fillable || !ValidateUse( from, false ) )
                return;

            if( targ is BaseBeverage )
            {
                BaseBeverage bev = (BaseBeverage)targ;

                if( bev.IsEmpty || !bev.ValidateUse( from, true ) )
                    return;

                this.Content = bev.Content;
                this.Poison = bev.Poison;
                this.Poisoner = bev.Poisoner;

                if( bev.Quantity > this.MaxQuantity )
                {
                    this.Quantity = this.MaxQuantity;
                    bev.Quantity -= this.MaxQuantity;
                }
                else
                {
                    this.Quantity += bev.Quantity;
                    bev.Quantity = 0;
                }
            }
            else if( targ is BaseWaterContainer )
            {
                BaseWaterContainer bwc = targ as BaseWaterContainer;

                if( Quantity == 0 || Content == BeverageType.Water && !IsFull )
                {
                    int iNeed = Math.Min( MaxQuantity - Quantity, bwc.Quantity );

                    if( iNeed > 0 && !bwc.IsEmpty && !IsFull )
                    {
                        bwc.Quantity -= iNeed;
                        Quantity += iNeed;
                        Content = BeverageType.Water;

                        from.PlaySound( 0x4E );
                    }
                }
            }
            else if( targ is Item )
            {
                Item item = (Item)targ;
                IWaterSource src;

                src = item as IWaterSource;

                if( src == null && item is AddonComponent )
                    src = ( (AddonComponent)item ).Addon as IWaterSource;

                if( src == null || src.Quantity <= 0 )
                    return;

                if( from.Map != item.Map || !from.InRange( item.GetWorldLocation(), 2 ) || !from.InLOS( item ) )
                {
                    from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
                    return;
                }

                this.Content = BeverageType.Water;
                this.Poison = null;
                this.Poisoner = null;

                if( src.Quantity > this.MaxQuantity )
                {
                    this.Quantity = this.MaxQuantity;
                    src.Quantity -= this.MaxQuantity;
                }
                else
                {
                    this.Quantity += src.Quantity;
                    src.Quantity = 0;
                }

                from.SendLocalizedMessage( 1010089 ); // You fill the container with water.
            }
            else if( targ is Cow )
            {
                Cow cow = (Cow)targ;

                if( cow.TryMilk( from ) )
                {
                    Content = BeverageType.Milk;
                    Quantity = MaxQuantity;
                    from.SendLocalizedMessage( 1080197 ); // You fill the container with milk.
                }
            }
        }

        private static int[] m_SwampTiles = new int[]
        {
            0x9C4, 0x9EB,
            0x3D65, 0x3D65,
            0x3DC0, 0x3DD9,
            0x3DDB, 0x3DDC,
            0x3DDE, 0x3EF0,
            0x3FF6, 0x3FF6,
            0x3FFC, 0x3FFE,
        };

        #region Effects of achohol
        private static Hashtable m_Table = new Hashtable();

        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler( EventSink_Login );
        }

        private static void EventSink_Login( LoginEventArgs e )
        {
            CheckHeaveTimer( e.Mobile );
        }

        public static void CheckHeaveTimer( Mobile from )
        {
            if( from.BAC > 0 && from.Map != Map.Internal && !from.Deleted )
            {
                Timer t = (Timer)m_Table[ from ];

                if( t == null )
                {
                    if( from.BAC > 60 )
                        from.BAC = 60;

                    t = new HeaveTimer( from );
                    t.Start();

                    m_Table[ from ] = t;
                }
            }
            else
            {
                Timer t = (Timer)m_Table[ from ];

                if( t != null )
                {
                    t.Stop();
                    m_Table.Remove( from );

                    from.SendLocalizedMessage( 500850 ); // You feel sober.
                }
            }
        }

        private class HeaveTimer : Timer
        {
            private Mobile m_Drunk;

            public HeaveTimer( Mobile drunk )
                : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
            {
                m_Drunk = drunk;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if( m_Drunk.Deleted || m_Drunk.Map == Map.Internal )
                {
                    Stop();
                    m_Table.Remove( m_Drunk );
                }
                else if( m_Drunk.Alive )
                {
                    if( m_Drunk.BAC > 60 )
                        m_Drunk.BAC = 60;

                    // chance to get sober
                    if( 10 > Utility.Random( 100 ) )
                        --m_Drunk.BAC;

                    // lose some stats
                    m_Drunk.Stam -= 1;
                    m_Drunk.Mana -= 1;

                    if( Utility.Random( 1, 4 ) == 1 )
                    {
                        if( !m_Drunk.Mounted )
                        {
                            // turn in a random direction
                            m_Drunk.Direction = (Direction)Utility.Random( 8 );

                            // heave
                            m_Drunk.Animate( 32, 5, 1, true, false, 0 );
                        }

                        // *hic*
                        m_Drunk.PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, 500849 );
                    }

                    if( m_Drunk.BAC <= 0 )
                    {
                        Stop();
                        m_Table.Remove( m_Drunk );

                        m_Drunk.SendLocalizedMessage( 500850 ); // You feel sober.
                    }
                }
            }
        }

        #endregion

        public virtual void Pour_OnTarget( Mobile from, object targ )
        {
            if( IsEmpty || !Pourable || !ValidateUse( from, false ) )
                return;

            if( targ is BaseBeverage )
            {
                BaseBeverage bev = (BaseBeverage)targ;

                if( !bev.ValidateUse( from, true ) )
                    return;

                if( bev.IsFull && bev.Content == this.Content )
                {
                    from.SendLocalizedMessage( 500848 ); // Couldn't pour it there.  It was already full.
                }
                else if( !bev.IsEmpty )
                {
                    from.SendLocalizedMessage( 500846 ); // Can't pour it there.
                }
                else
                {
                    bev.Content = this.Content;
                    bev.Poison = this.Poison;
                    bev.Poisoner = this.Poisoner;

                    if( this.Quantity > bev.MaxQuantity )
                    {
                        bev.Quantity = bev.MaxQuantity;
                        this.Quantity -= bev.MaxQuantity;
                    }
                    else
                    {
                        bev.Quantity += this.Quantity;
                        this.Quantity = 0;
                    }

                    from.PlaySound( 0x4E );
                }
            }
            else if( from == targ )
            {
                if( from.Thirst < 20 )
                    from.Thirst += 1;

                if( ContainsAlchohol )
                {
                    int bac = 0;

                    switch( this.Content )
                    {
                        case BeverageType.Ale: bac = 1; break;
                        case BeverageType.Wine: bac = 2; break;
                        case BeverageType.Cider: bac = 3; break;
                        case BeverageType.Liquor: bac = 4; break;
                    }

                    from.BAC += bac;

                    if( from.BAC > 60 )
                        from.BAC = 60;

                    CheckHeaveTimer( from );
                }

                from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );

                if( m_Poison != null )
                    from.ApplyPoison( m_Poisoner, m_Poison );

                --Quantity;
            }
            else if( targ is BaseWaterContainer )
            {
                BaseWaterContainer bwc = targ as BaseWaterContainer;
				
                if( Content != BeverageType.Water )
                {
                    from.SendLocalizedMessage( 500842 ); // Can't pour that in there.
                }
                else if( bwc.Items.Count != 0 )
                {
                    from.SendLocalizedMessage( 500841 ); // That has something in it.
                }
                else
                {				
                    int itNeeds = Math.Min( bwc.MaxQuantity - bwc.Quantity, Quantity );

                    if( itNeeds > 0 )
                    {
                        bwc.Quantity += itNeeds;
                        Quantity -= itNeeds;

                        from.PlaySound( 0x4E );
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage( 500846 ); // Can't pour it there.
            }
        }

        public override void OnDoubleClick( Mobile from )
        {
            if( IsEmpty )
            {
                if( !Fillable || !ValidateUse( from, true ) )
                    return;

                from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( Fill_OnTarget ) );
                SendLocalizedMessageTo( from, 500837 ); // Fill from what?
            }
            else if( Pourable && ValidateUse( from, true ) )
            {
                from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( Pour_OnTarget ) );
                from.SendLocalizedMessage( 1010086 ); // What do you want to use this on?
            }
        }

        public static bool ConsumeTotal( Container pack, BeverageType content, int quantity )
        {
            return ConsumeTotal( pack, typeof( BaseBeverage ), content, quantity );
        }

        public static bool ConsumeTotal( Container pack, Type itemType, BeverageType content, int quantity )
        {
            Item[] items = pack.FindItemsByType( itemType );

            // First pass, compute total
            int total = 0;

            for( int i = 0; i < items.Length; ++i )
            {
                BaseBeverage bev = items[ i ] as BaseBeverage;

                if( bev != null && bev.Content == content && !bev.IsEmpty )
                    total += bev.Quantity;
            }

            if( total >= quantity )
            {
                // We've enough, so consume it

                int need = quantity;

                for( int i = 0; i < items.Length; ++i )
                {
                    BaseBeverage bev = items[ i ] as BaseBeverage;

                    if( bev == null || bev.Content != content || bev.IsEmpty )
                        continue;

                    int theirQuantity = bev.Quantity;

                    if( theirQuantity < need )
                    {
                        bev.Quantity = 0;
                        need -= theirQuantity;
                    }
                    else
                    {
                        bev.Quantity -= need;
                        return true;
                    }
                }
            }

            return false;
        }

        public BaseBeverage()
        {
            ItemID = ComputeItemID();
        }

        public BaseBeverage( BeverageType type )
        {
            m_Content = type;
            m_Quantity = MaxQuantity;
            ItemID = ComputeItemID();
        }

        public BaseBeverage( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int)1 ); // version

            writer.Write( (Mobile)m_Poisoner );

            Poison.Serialize( m_Poison, writer );
            writer.Write( (int)m_Content );
            writer.Write( (int)m_Quantity );
        }

        protected bool CheckType( string name )
        {
            return World.LoadingType == String.Format( "Server.Items.{0}", name );
        }

        public override void Deserialize( GenericReader reader )
        {
            InternalDeserialize( reader, true );
        }

        protected void InternalDeserialize( GenericReader reader, bool read )
        {
            base.Deserialize( reader );

            if( !read )
                return;

            int version = reader.ReadInt();

            switch( version )
            {
                case 1:
                {
                    m_Poisoner = reader.ReadMobile();
                    goto case 0;
                }
                case 0:
                {
                    m_Poison = Poison.Deserialize( reader );
                    m_Content = (BeverageType)reader.ReadInt();
                    m_Quantity = reader.ReadInt();
                    break;
                }
            }
        }
    }
}