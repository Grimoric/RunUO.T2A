namespace Server.Mobiles
{
    [CorpseName( "a Saliva corpse" )]
	public class Saliva : Harpy
	{
		[Constructable]
		public Saliva()
		{
			// TODO: Not a paragon? No ML arties?
			// It moves like a paragon on OSI...

			Name = "Saliva";
			Hue = 0x11E;

			SetStr( 110, 206 );
			SetDex( 123, 222 );
			SetInt( 80, 127 );

			SetHits( 409, 842 );

			SetDamage( 20, 22 );

			SetSkill( SkillName.Wrestling, 106.4, 128.8 );
			SetSkill( SkillName.Tactics, 129.9, 141.0 );
			SetSkill( SkillName.MagicResist, 84.3, 105.0 );

			// TODO: Fame/Karma?
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public Saliva( Serial serial )
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
