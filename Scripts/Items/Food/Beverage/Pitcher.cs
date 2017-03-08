using System;

namespace Server.Items
{
    [TypeAlias( "Server.Items.PitcherAle", "Server.Items.PitcherCider", "Server.Items.PitcherLiquor",
        "Server.Items.PitcherMilk", "Server.Items.PitcherWine", "Server.Items.PitcherWater",
        "Server.Items.GlassPitcher" )]
    public class Pitcher : BaseBeverage
    {
        public override int BaseLabelNumber { get { return 1048128; } } // a Pitcher of Ale
        public override int MaxQuantity { get { return 5; } }

        public override int ComputeItemID()
        {
            if( IsEmpty )
            {
                if( ItemID == 0x9A7 || ItemID == 0xFF7 )
                    return ItemID;

                return 0xFF6;
            }

            switch( Content )
            {
                case BeverageType.Ale:
                {
                    if( ItemID == 0x1F96 )
                        return ItemID;

                    return 0x1F95;
                }
                case BeverageType.Cider:
                {
                    if( ItemID == 0x1F98 )
                        return ItemID;

                    return 0x1F97;
                }
                case BeverageType.Liquor:
                {
                    if( ItemID == 0x1F9A )
                        return ItemID;

                    return 0x1F99;
                }
                case BeverageType.Milk:
                {
                    if( ItemID == 0x9AD )
                        return ItemID;

                    return 0x9F0;
                }
                case BeverageType.Wine:
                {
                    if( ItemID == 0x1F9C )
                        return ItemID;

                    return 0x1F9B;
                }
                case BeverageType.Water:
                {
                    if( ItemID == 0xFF8 || ItemID == 0xFF9 || ItemID == 0x1F9E )
                        return ItemID;

                    return 0x1F9D;
                }
            }

            return 0;
        }

        [Constructable]
        public Pitcher()
        {
            Weight = 2.0;
        }

        [Constructable]
        public Pitcher( BeverageType type )
            : base( type )
        {
            Weight = 2.0;
        }

        public Pitcher( Serial serial )
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
            if( CheckType( "PitcherWater" ) || CheckType( "GlassPitcher" ) )
                base.InternalDeserialize( reader, false );
            else
                base.InternalDeserialize( reader, true );

            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                {
                    if( CheckType( "PitcherAle" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Ale;
                    }
                    else if( CheckType( "PitcherCider" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Cider;
                    }
                    else if( CheckType( "PitcherLiquor" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Liquor;
                    }
                    else if( CheckType( "PitcherMilk" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Milk;
                    }
                    else if( CheckType( "PitcherWine" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Wine;
                    }
                    else if( CheckType( "PitcherWater" ) )
                    {
                        Quantity = MaxQuantity;
                        Content = BeverageType.Water;
                    }
                    else if( CheckType( "GlassPitcher" ) )
                    {
                        Quantity = 0;
                        Content = BeverageType.Water;
                    }
                    else
                    {
                        throw new Exception( World.LoadingType );
                    }

                    break;
                }
            }
        }
    }
}