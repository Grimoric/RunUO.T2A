namespace Server.Mobiles
{
    [CorpseName( "a Lady Sabrix corpse" )]
	public class LadySabrix : GiantBlackWidow
	{
		[Constructable]
		public LadySabrix()
		{
			IsParagon = true;

			Name = "Lady Sabrix";
			Hue = 0x497;

			SetStr( 82, 130 );
			SetDex( 117, 146 );
			SetInt( 50, 98 );

			SetHits( 233, 361 );
			SetStam( 117, 146 );
			SetMana( 50, 98 );

			SetDamage( 15, 22 );

			SetSkill( SkillName.Wrestling, 109.8, 122.8 );
			SetSkill( SkillName.Tactics, 102.8, 120.0 );
			SetSkill( SkillName.MagicResist, 79.4, 95.1 );
			SetSkill( SkillName.Anatomy, 68.8, 105.1 );
			SetSkill( SkillName.Poisoning, 97.8, 116.7 );

			Fame = 18900;
			Karma = -18900;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public LadySabrix( Serial serial )
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
