namespace Server.Mobiles
{
    [CorpseName( "a Lady Lissith corpse" )]
	public class LadyLissith : GiantBlackWidow
	{
		[Constructable]
		public LadyLissith()
		{
			IsParagon = true;

			Name = "Lady Lissith";
			Hue = 0x452;

			SetStr( 81, 130 );
			SetDex( 116, 152 );
			SetInt( 44, 100 );

			SetHits( 245, 375 );
			SetStam( 116, 152 );
			SetMana( 44, 100 );

			SetDamage( 15, 22 );

			SetSkill( SkillName.Wrestling, 108.6, 123.0 );
			SetSkill( SkillName.Tactics, 102.7, 119.0 );
			SetSkill( SkillName.MagicResist, 78.8, 95.6 );
			SetSkill( SkillName.Anatomy, 68.6, 106.8 );
			SetSkill( SkillName.Poisoning, 96.6, 112.9 );

			Fame = 18900;
			Karma = -18900;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public LadyLissith( Serial serial )
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
