namespace Server.Mobiles
{
    [CorpseName( "a Lurg corpse" )]
	public class Lurg : Troglodyte
	{
		[Constructable]
		public Lurg()
		{
			IsParagon = true;

			Name = "Lurg";
			Hue = 0x455;

			SetStr( 584, 625 );
			SetDex( 163, 176 );
			SetInt( 90, 106 );

			SetHits( 3034, 3189 );
			SetStam( 163, 176 );
			SetMana( 90, 106 );

			SetDamage( 16, 19 );

			SetSkill( SkillName.Wrestling, 122.7, 130.5 );
			SetSkill( SkillName.Tactics, 109.3, 118.5 );
			SetSkill( SkillName.MagicResist, 72.9, 87.6 );
			SetSkill( SkillName.Anatomy, 110.5, 124.0 );
			SetSkill( SkillName.Healing, 84.1, 105.0 );

			Fame = 10000;
			Karma = -10000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override int TreasureMapLevel{ get{ return 4; } }

		public Lurg( Serial serial )
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
