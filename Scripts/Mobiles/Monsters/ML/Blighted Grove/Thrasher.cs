using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a Thrasher corpse" )]
	public class Thrasher : Alligator
	{
		[Constructable]
		public Thrasher()
		{
			IsParagon = true;

			Name = "Thrasher";
			Hue = 0x497;

			SetStr( 93, 327 );
			SetDex( 7, 201 );
			SetInt( 15, 67 );

			SetHits( 260, 984 );
			SetStam( 56, 75 );
			SetMana( 25, 30 );

			SetDamage( 20, 30 );

			SetSkill( SkillName.Wrestling, 101.2, 118.3 );
			SetSkill( SkillName.Tactics, 96.3, 117.3 );
			SetSkill( SkillName.MagicResist, 102.4, 118.6 );

			// TODO: Fame/Karma
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
		}
        
		public override int Hides { get { return 48; } }
		public override int Meat { get { return 1; } }

		public Thrasher( Serial serial )
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
