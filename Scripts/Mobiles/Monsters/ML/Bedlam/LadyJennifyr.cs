using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName( "a Lady Jennifyr corpse" )]
	public class LadyJennifyr : SkeletalKnight
	{
		[Constructable]
		public LadyJennifyr()
		{
			IsParagon = true;

			Name = "Lady Jennifyr";
			Hue = 0x76D;

			SetStr( 208, 309 );
			SetDex( 91, 118 );
			SetInt( 44, 101 );

			SetHits( 1113, 1285 );

			SetDamage( 15, 25 );

			SetSkill( SkillName.Wrestling, 127.9, 137.1 );
			SetSkill( SkillName.Tactics, 128.4, 141.9 );
			SetSkill( SkillName.MagicResist, 102.1, 119.5 );
			SetSkill( SkillName.Anatomy, 129.0, 137.5 );

			Fame = 18000;
			Karma = -18000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public LadyJennifyr( Serial serial )
			: base( serial )
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
	}
}
