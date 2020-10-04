using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBTailor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Sewing kit", typeof( SewingKit ), 3, 20, 0xF9D, 0 ) ); 
				Add( new GenericBuyInfo( "Scissors", typeof( Scissors ), 11, 20, 0xF9F, 0 ) );
				Add( new GenericBuyInfo( "Dying tub", typeof( DyeTub ), 8, 20, 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( "Dyes", typeof( Dyes ), 8, 20, 0xFA9, 0 ) ); 

				Add( new GenericBuyInfo( "Shirt", typeof( Shirt ), 12, 20, 0x1517, 0 ) );
				Add( new GenericBuyInfo( "Short pants", typeof( ShortPants ), 7, 20, 0x152E, 0 ) );
				Add( new GenericBuyInfo( "Fancy shirt", typeof( FancyShirt ), 21, 20, 0x1EFD, 0 ) );
				Add( new GenericBuyInfo( "Long pants", typeof( LongPants ), 10, 20, 0x1539, 0 ) );
				Add( new GenericBuyInfo( "Fancy dress", typeof( FancyDress ), 26, 20, 0x1EFF, 0 ) );
				Add( new GenericBuyInfo( "Plain dress",  typeof( PlainDress ), 13, 20, 0x1F01, 0 ) );
				Add( new GenericBuyInfo( "Kilt", typeof( Kilt ), 11, 20, 0x1537, 0 ) );
				Add( new GenericBuyInfo( "Kilt", typeof( Kilt ), 11, 20, 0x1537, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Half apron", typeof( HalfApron ), 10, 20, 0x153b, 0 ) );
				Add( new GenericBuyInfo( "Robe", typeof( Robe ), 18, 20, 0x1F03, 0 ) );
				Add( new GenericBuyInfo( "Cloak", typeof( Cloak ), 8, 20, 0x1515, 0 ) );
				Add( new GenericBuyInfo( "Cloak", typeof( Cloak ), 8, 20, 0x1515, 0 ) );
				Add( new GenericBuyInfo( "Doublet", typeof( Doublet ), 13, 20, 0x1F7B, 0 ) );
				Add( new GenericBuyInfo( "Tunic", typeof( Tunic ), 18, 20, 0x1FA1, 0 ) );
				Add( new GenericBuyInfo( "Jester suit", typeof( JesterSuit ), 26, 20, 0x1F9F, 0 ) );

				Add( new GenericBuyInfo( "Jester hat", typeof( JesterHat ), 12, 20, 0x171C, 0 ) );
				Add( new GenericBuyInfo( "Floppy hat",  typeof( FloppyHat ), 7, 20, 0x1713, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Wide-brim hat", typeof( WideBrimHat ), 8, 20, 0x1714, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Cap", typeof( Cap ), 10, 20, 0x1715, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Tall straw hat", typeof( TallStrawHat ), 8, 20, 0x1716, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Straw hat", typeof( StrawHat ), 7, 20, 0x1717, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Wizard's hat", typeof( WizardsHat ), 11, 20, 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Leather cap", typeof( LeatherCap ), 10, 20, 0x1DB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Feathered hat", typeof( FeatheredHat ), 10, 20, 0x171A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Tricorne hat", typeof( TricorneHat ), 8, 20, 0x171B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Bandana", typeof( Bandana ), 6, 20, 0x1540, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( "Skullcap", typeof( SkullCap ), 7, 20, 0x1544, Utility.RandomDyedHue() ) );

				Add( new GenericBuyInfo( "Bolt of cloth", typeof( BoltOfCloth ), 100, 20, 0xf95, Utility.RandomDyedHue() ) ); 

				Add( new GenericBuyInfo( "Cut cloth", typeof( Cloth ), 2, 20, 0x1766, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( "Cloth", typeof( UncutCloth ), 2, 20, 0x1767, Utility.RandomDyedHue() ) ); 

				Add( new GenericBuyInfo( "Bale of cotton", typeof( Cotton ), 102, 20, 0xDF9, 0 ) );
				Add( new GenericBuyInfo( "Pile of wool", typeof( Wool ), 62, 20, 0xDF8, 0 ) );
				Add( new GenericBuyInfo( "Flax bundle", typeof( Flax ), 102, 20, 0x1A9C, 0 ) );
				Add( new GenericBuyInfo( "Spool of thread", typeof( SpoolOfThread ), 18, 20, 0xFA0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Scissors ), 6 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Dyes ), 4 );
				Add( typeof( DyeTub ), 4 );

				Add( typeof( BoltOfCloth ), 50 );

				Add( typeof( FancyShirt ), 10 );
				Add( typeof( Shirt ), 6 );

				Add( typeof( ShortPants ), 3 );
				Add( typeof( LongPants ), 5 );

				Add( typeof( Cloak ), 4 );
				Add( typeof( FancyDress ), 12 );
				Add( typeof( Robe ), 9 );
				Add( typeof( PlainDress ), 7 );

				Add( typeof( Skirt ), 5 );
				Add( typeof( Kilt ), 5 );

				Add( typeof( Doublet ), 7 );
				Add( typeof( Tunic ), 9 );
				Add( typeof( JesterSuit ), 13 );

				Add( typeof( FullApron ), 5 );
				Add( typeof( HalfApron ), 5 );

				Add( typeof( JesterHat ), 6 );
				Add( typeof( FloppyHat ), 3 );
				Add( typeof( WideBrimHat ), 4 );
				Add( typeof( Cap ), 5 );
				Add( typeof( SkullCap ), 3 );
				Add( typeof( Bandana ), 3 );
				Add( typeof( TallStrawHat ), 4 );
				Add( typeof( StrawHat ), 4 );
				Add( typeof( WizardsHat ), 5 );
				Add( typeof( Bonnet ), 4 );
				Add( typeof( FeatheredHat ), 5 );
				Add( typeof( TricorneHat ), 4 );

				Add( typeof( SpoolOfThread ), 9 );

				Add( typeof( Flax ), 51 );
				Add( typeof( Cotton ), 51 );
				Add( typeof( Wool ), 31 );
			}
		}
	}
}
