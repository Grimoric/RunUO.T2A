namespace Server.Mobiles
{
    [CorpseName( "a Rend corpse" )]
	public class Rend : Reptalon
	{
		[Constructable]
		public Rend()
		{
			IsParagon = true;

			Name = "Rend";
			Hue = 0x455;

			SetStr( 1261, 1284 );
			SetDex( 363, 384 );
			SetInt( 601, 642 );

			SetHits( 5176, 6100 );

			SetDamage( 26, 33 );

			SetSkill( SkillName.Wrestling, 136.3, 150.3 );
			SetSkill( SkillName.Tactics, 133.4, 141.4 );
			SetSkill( SkillName.MagicResist, 90.9, 110.0 );
			SetSkill( SkillName.Anatomy, 66.6, 72.0 );

			Fame = 21000;
			Karma = -21000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public Rend( Serial serial )
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
