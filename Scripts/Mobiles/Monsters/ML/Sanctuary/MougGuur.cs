namespace Server.Mobiles
{
    [CorpseName( "a Moug-Guur corpse" )]
	public class MougGuur : Ettin
	{
		[Constructable]
		public MougGuur()
		{
			Name = "Moug-Guur";

			SetStr( 556, 575 );
			SetDex( 84, 94 );
			SetInt( 59, 73 );

			SetHits( 400, 415 );

			SetDamage( 12, 20 );

			SetSkill( SkillName.MagicResist, 70.2, 75.0 );
			SetSkill( SkillName.Tactics, 80.8, 81.7 );
			SetSkill( SkillName.Wrestling, 93.9, 99.4 );

			Fame = 3000;
			Karma = -3000;
		}

		public MougGuur( Serial serial )
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
