using System;
using Server;

namespace Server.Misc
{
	public class RenameRequests
	{
		public static void Initialize()
		{
			EventSink.RenameRequest += new RenameRequestEventHandler( EventSink_RenameRequest );
		}

		private static void EventSink_RenameRequest( RenameRequestEventArgs e )
		{
			Mobile from = e.From;
			Mobile targ = e.Target;
			string name = e.Name;

			if ( from.CanSee( targ ) && from.InRange( targ, 12 ) && targ.CanBeRenamedBy( from ) )
			{
				name = name.Trim();

				if( NameVerification.Validate( name, 1, 16, true, false, true, 0, NameVerification.Empty, NameVerification.StartDisallowed, new string[]{} ) )
				{
					targ.Name = name;
				}
				else
				{
					from.SendMessage( "That name is unacceptable." );
				}
			}
		}
	}
}