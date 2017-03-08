namespace Server.Mobiles
{
    [CorpseName("a mongbat corpse")]
    public class StrongMongbat : BaseCreature
    {
        [Constructable]
        public StrongMongbat() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a mongbat";
            Body = 39;
            BaseSoundID = 422;

            SetStr(56, 80);
            SetDex(61, 80);
            SetInt(26, 50);

            SetHits(34, 48);
            SetMana(25, 50);

            SetDamage(3, 9);

            SetSkill(SkillName.MagicResist, 15.1, 30.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 20.1, 35.0);

            Fame = 150;
            Karma = -150;

            VirtualArmor = 10;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 71.1;
        }

        public override bool CanFly { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
        }

        public override int Meat { get { return 1; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }

        public StrongMongbat(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
