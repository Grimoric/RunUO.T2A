using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server
{
    public class ValorVirtue
	{
		private static TimeSpan LossDelay = TimeSpan.FromDays( 7.0 );
		private const int LossAmount = 250;

		public static void Initialize()
		{
			VirtueGump.Register( 112, new OnVirtueUsed( OnVirtueUsed ) );
		}

		public static void OnVirtueUsed( Mobile from )
		{
			if( from.Alive )
			{
				from.SendLocalizedMessage( 1054034 ); // Target the Champion Idol of the Champion you wish to challenge!.
				from.Target = new InternalTarget();
			}
		}

		public static void CheckAtrophy( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

			if( pm == null )
				return;

			try
			{
				if( pm.LastValorLoss + LossDelay < DateTime.Now )
				{
					if( VirtueHelper.Atrophy( from, VirtueName.Valor, LossAmount ) )
						from.SendLocalizedMessage( 1054040 ); // You have lost some Valor.

					pm.LastValorLoss = DateTime.Now;
				}
			}
			catch
			{
			}
		}

		public static void Valor( Mobile from, object targ )
		{
		}

		private class InternalTarget : Target
		{
			public InternalTarget()	: base( 14, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Valor( from, targeted );
			}
		}
	}
}