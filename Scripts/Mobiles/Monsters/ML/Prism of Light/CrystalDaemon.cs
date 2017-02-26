namespace Server.Mobiles
{
    [CorpseName( "a crystal daemon corpse" )]
	public class CrystalDaemon : BaseCreature
	{
		[Constructable]
		public CrystalDaemon()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal daemon";
			Body = 0x310;
			Hue = 0x3E8;
			BaseSoundID = 0x47D;

			SetStr( 140, 200 );
			SetDex( 120, 150 );
			SetInt( 800, 850 );

			SetHits( 200, 220 );

			SetDamage( 16, 20 );

			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 70.0, 80.0 );
			SetSkill( SkillName.MagicResist, 100.0, 110.0 );
			SetSkill( SkillName.Magery, 120.0, 130.0 );
			SetSkill( SkillName.EvalInt, 100.0, 110.0 );
			SetSkill( SkillName.Meditation, 100.0, 110.0 );

			Fame = 15000;
			Karma = -15000;

			PackArcaneScroll( 0, 1 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}

		/*
		// TODO: uncomment once added
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ScatteredCrystals() );
		}
		*/

		public CrystalDaemon( Serial serial )
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
