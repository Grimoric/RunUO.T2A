using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBCook : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBCook() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{
				Add(new GenericBuyInfo( "Loaf of bread", typeof( BreadLoaf ), 6, 10, 0x103B, 0 ) );
				Add(new GenericBuyInfo( "Loaf of bread", typeof( BreadLoaf ), 5, 20, 0x103C, 0 ) );
				Add(new GenericBuyInfo( "Baked pie", typeof( ApplePie ), 7, 20, 0x1041, 0 ) );
				Add(new GenericBuyInfo( "Cake", typeof( Cake ), 7, 20, 0x9E9, 0 ) );
				Add(new GenericBuyInfo( "Muffin", typeof( Muffins ), 7, 20, 0x9EA, 0 ) );

				Add(new GenericBuyInfo( "Wheel of cheese", typeof( CheeseWheel ), 21, 10, 0x97E, 0 ) );
				Add(new GenericBuyInfo( "Cooked bird", typeof( CookedBird ), 17, 20, 0x9B7, 0 ) );
				Add(new GenericBuyInfo( "Cooked leg of lamb", typeof( LambLeg ), 8, 20, 0x160A, 0 ) );
				Add(new GenericBuyInfo( "Chicken leg", typeof( ChickenLeg ), 5, 20, 0x1608, 0 ) );

				Add(new GenericBuyInfo( "Bowl of carrots", typeof( WoodenBowlOfCarrots ), 3, 20, 0x15F9, 0 ) );
				Add(new GenericBuyInfo( "Bowl of corn", typeof( WoodenBowlOfCorn ), 3, 20, 0x15FA, 0 ) );
				Add(new GenericBuyInfo( "Bowl of lettuce", typeof( WoodenBowlOfLettuce ), 3, 20, 0x15FB, 0 ) );
				Add(new GenericBuyInfo( "Bowl of peas", typeof( WoodenBowlOfPeas ), 3, 20, 0x15FC, 0 ) );
				Add(new GenericBuyInfo( "Pewter bowl", typeof( EmptyPewterBowl ), 2, 20, 0x15FD, 0 ) );
				Add(new GenericBuyInfo( "Bowl of corn", typeof( PewterBowlOfCorn ), 3, 20, 0x15FE, 0 ) );
				Add(new GenericBuyInfo( "Bowl of lettuce", typeof( PewterBowlOfLettuce ), 3, 20, 0x15FF, 0 ) );
				Add(new GenericBuyInfo( "Bowl of peas", typeof( PewterBowlOfPeas ), 3, 20, 0x1600, 0 ) );
				Add(new GenericBuyInfo( "Bowl of potatos", typeof( PewterBowlOfPotatos ), 3, 20, 0x1601, 0 ) );
				Add(new GenericBuyInfo( "Bowl pf stew", typeof( WoodenBowlOfStew ), 3, 20, 0x1604, 0 ) );
				Add(new GenericBuyInfo( "Bowl of tomato soup", typeof( WoodenBowlOfTomatoSoup ), 3, 20, 0x1606, 0 ) );

				Add( new GenericBuyInfo( "Roast pig", typeof( RoastPig ), 106, 20, 0x9BB, 0 ) );
				Add( new GenericBuyInfo( "Sack of flour", typeof( SackFlour ), 3, 20, 0x1039, 0 ) );
				Add( new GenericBuyInfo( "Jar of honey", typeof( JarHoney ), 3, 20, 0x9EC, 0 ) );
				Add( new GenericBuyInfo( "Rolling pin", typeof( RollingPin ), 2, 20, 0x1043, 0 ) );
				Add( new GenericBuyInfo( "Flour sifter", typeof( FlourSifter ), 2, 20, 0x103E, 0 ) );
				Add( new GenericBuyInfo( "Skillet", typeof( Skillet ), 3, 20, 0x97F, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( CheeseWheel ), 12 );
				Add( typeof( CookedBird ), 8 );
				Add( typeof( RoastPig ), 53 );
				Add( typeof( Cake ), 5 );
				Add( typeof( JarHoney ), 1 );
				Add( typeof( SackFlour ), 1 );
				Add( typeof( BreadLoaf ), 2 );
				Add( typeof( ChickenLeg ), 3 );
				Add( typeof( LambLeg ), 4 );
				Add( typeof( Skillet ), 1 );
				Add( typeof( FlourSifter ), 1 );
				Add( typeof( RollingPin ), 1 );
				Add( typeof( Muffins ), 1 );
				Add( typeof( ApplePie ), 3 );

				Add( typeof( WoodenBowlOfCarrots ), 1 );
				Add( typeof( WoodenBowlOfCorn ), 1 );
				Add( typeof( WoodenBowlOfLettuce ), 1 );
				Add( typeof( WoodenBowlOfPeas ), 1 );
				Add( typeof( EmptyPewterBowl ), 1 );
				Add( typeof( PewterBowlOfCorn ), 1 );
				Add( typeof( PewterBowlOfLettuce ), 1 );
				Add( typeof( PewterBowlOfPeas ), 1 );
				Add( typeof( PewterBowlOfPotatos ), 1 );
				Add( typeof( WoodenBowlOfStew ), 1 );
				Add( typeof( WoodenBowlOfTomatoSoup ), 1 );
			} 
		} 
	} 
}