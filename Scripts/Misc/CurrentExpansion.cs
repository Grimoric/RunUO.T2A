using System;

namespace Server
{
    public class CurrentExpansion
	{
		public static void Configure()
		{
			Mobile.InsuranceEnabled = false;
			ObjectPropertyList.Enabled = false;
			Mobile.VisibleDamageType = VisibleDamageType.None;
			Mobile.GuildClickMessage = true;
			Mobile.AsciiClickMessage = true;

			// OSI-style action delay
			Mobile.ActionDelay = TimeSpan.FromSeconds( 1.0 );
		}
	}
}
