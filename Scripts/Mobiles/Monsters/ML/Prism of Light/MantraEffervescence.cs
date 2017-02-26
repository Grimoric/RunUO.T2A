namespace Server.Mobiles
{
    [CorpseName( "a mantra effervescence corpse" )]
	public class MantraEffervescence : BaseCreature
	{
		[Constructable]
		public MantraEffervescence()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mantra effervescence";
			Body = 0x111;
			BaseSoundID = 0x56E;

			SetStr( 130, 150 );
			SetDex( 120, 130 );
			SetInt( 150, 230 );

			SetHits( 150, 250 );

			SetDamage( 21, 25 );

			SetSkill( SkillName.Wrestling, 80.0, 85.0 );
			SetSkill( SkillName.Tactics, 80.0, 85.0 );
			SetSkill( SkillName.MagicResist, 105.0, 115.0 );
			SetSkill( SkillName.Magery, 90.0, 110.0 );
			SetSkill( SkillName.EvalInt, 80.0, 90.0 );
			SetSkill( SkillName.Meditation, 90.0, 100.0 );

			Fame = 6500;
			Karma = -6500;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}

		public MantraEffervescence( Serial serial )
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
