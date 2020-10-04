using System.Collections.Generic;
using Server.Multis;

namespace Server.Mobiles
{
    public class SBShipwright : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBShipwright()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo("Small ship deed", typeof( SmallBoatDeed ), 10177, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo("Small dragon ship deed", typeof( SmallDragonBoatDeed ), 10177, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo("Medium ship deed", typeof( MediumBoatDeed ), 11552, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo("Medium dragon ship deed", typeof( MediumDragonBoatDeed ), 11552, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo("Large ship deed", typeof( LargeBoatDeed ), 12927, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo("Large dragon ship deed", typeof( LargeDragonBoatDeed ), 12927, 20, 0x14F2, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//You technically CAN sell them back, *BUT* the vendors do not carry enough money to buy with
			}
		}
	}
}