using System;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;

namespace Server.SkillHandlers
{
    public class DetectHidden
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.DetectHidden].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile src )
		{
			src.SendLocalizedMessage( 500819 );//Where will you search?
			src.Target = new InternalTarget();

			return TimeSpan.FromSeconds( 6.0 );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 12, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile src, object targ )
			{
				bool foundAnyone = false;

				Point3D p;
				if ( targ is Mobile )
					p = ((Mobile)targ).Location;
				else if ( targ is Item )
					p = ((Item)targ).Location;
				else if ( targ is IPoint3D )
					p = new Point3D( (IPoint3D)targ );
				else 
					p = src.Location;

				double srcSkill = src.Skills[SkillName.DetectHidden].Value;
				int range = (int)(srcSkill / 10.0);

				if ( !src.CheckSkill( SkillName.DetectHidden, 0.0, 100.0 ) )
					range /= 2;

				BaseHouse house = BaseHouse.FindHouseAt( p, src.Map, 16 );

				bool inHouse = house != null && house.IsFriend( src );

				if ( inHouse )
					range = 22;

				if ( range > 0 )
				{
					IPooledEnumerable inRange = src.Map.GetMobilesInRange( p, range );

					foreach ( Mobile trg in inRange )
					{
						if ( trg.Hidden && src != trg )
						{
							double ss = srcSkill + Utility.Random( 21 ) - 10;
							double ts = trg.Skills[SkillName.Hiding].Value + Utility.Random( 21 ) - 10;

							if ( src.AccessLevel >= trg.AccessLevel && ( ss >= ts || inHouse && house.IsInside( trg ) ) )
							{
								trg.RevealingAction();
								trg.SendLocalizedMessage( 500814 ); // You have been revealed!
								foundAnyone = true;
							}
						}
					}

					inRange.Free();
				}

				if ( !foundAnyone )
				{
					src.SendLocalizedMessage( 500817 ); // You can see nothing hidden there.
				}
			}
		}
	}
}
