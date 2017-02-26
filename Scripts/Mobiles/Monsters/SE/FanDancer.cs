using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "a fan dancer corpse" )]
	public class FanDancer : BaseCreature
	{
		[Constructable]
		public FanDancer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fan dancer";
			Body = 247;
			BaseSoundID = 0x372;

			SetStr( 301, 375 );
			SetDex( 201, 255 );
			SetInt( 21, 25 );

			SetHits( 351, 430 );

			SetDamage( 12, 17 );

			SetSkill( SkillName.MagicResist, 100.1, 110.0 );
			SetSkill( SkillName.Tactics, 85.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 95.0 );
			SetSkill( SkillName.Anatomy, 85.1, 95.0 );

			Fame = 9000;
			Karma = -9000;
			
			if ( Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
				
			AddItem( new Tessen() );
			
			if ( 0.02 >= Utility.RandomDouble() )
				PackItem( new OrigamiPaper() );
		}
				
				
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}
			
		public override bool Uncalmable{ get{ return true; } }

		/* TODO: Repel Magic
		 * 10% chance of repelling a melee attack (why did they call it repel magic anyway?)
		 * Cliloc: 1070844
		 * Effect: damage is dealt to the attacker, no damage is taken by the fan dancer
		 */

		public override void OnDamagedBySpell( Mobile attacker )
		{
			base.OnDamagedBySpell( attacker );

			if ( 0.8 > Utility.RandomDouble() && !attacker.InRange( this, 1 ) )
			{
				/* Fan Throw
				 * Effect: - To: "0x57D4F5B" - ItemId: "0x27A3" - ItemIdName: "Tessen" - FromLocation: "(992 299, 24)" - ToLocation: "(992 308, 22)" - Speed: "10" - Duration: "0" - FixedDirection: "False" - Explode: "False" - Hue: "0x0" - Render: "0x0"
				 * Damage: 50-65
				 */
				Effects.SendPacket( attacker, attacker.Map, new HuedEffect( EffectType.Moving, Serial.Zero, Serial.Zero, 0x27A3, this.Location, attacker.Location, 10, 0, false, false, 0, 0 ) );
				AOS.Damage( attacker, this, Utility.RandomMinMax( 50, 65 ), 100, 0, 0, 0, 0 );
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.8 > Utility.RandomDouble() && !attacker.InRange( this, 1 ) )
			{
				Effects.SendPacket( attacker, attacker.Map, new HuedEffect( EffectType.Moving, Serial.Zero, Serial.Zero, 0x27A3, this.Location, attacker.Location, 10, 0, false, false, 0, 0 ) );
				AOS.Damage( attacker, this, Utility.RandomMinMax( 50, 65 ), 100, 0, 0, 0, 0 );
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public FanDancer( Serial serial ) : base( serial )
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