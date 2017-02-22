using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public class TinkerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public TinkerGuildmaster() : base( "tinker" )
		{
			SetSkill( SkillName.Lockpicking, 65.0, 88.0 );
			SetSkill( SkillName.Tinkering, 90.0, 100.0 );
			SetSkill( SkillName.RemoveTrap, 85.0, 100.0 );
		}

		public TinkerGuildmaster( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		private class RechargeEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private Mobile m_Vendor;

			public RechargeEntry( Mobile from, Mobile vendor ) : base( 6271, 6 )
			{
				m_From = from;
				m_Vendor = vendor;
			}

			public override void OnClick()
			{
			}
		}
	}
}