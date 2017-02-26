namespace Server
{
    public class AOS
    {
        public static int Damage(Mobile m, int damage, int phys, int fire, int cold, int pois, int nrgy)
        {
            return Damage(m, null, damage, phys, fire, cold, pois, nrgy);
        }

        public static int Damage(Mobile m, Mobile from, int damage, int phys, int fire, int cold, int pois, int nrgy)
        {
            return Damage(m, from, damage, false, phys, fire, cold, pois, nrgy, 0, 0, false, false, false);
        }

        public static int Damage(Mobile m, Mobile from, int damage, int phys, int fire, int cold, int pois, int nrgy, int chaos)
        {
            return Damage(m, from, damage, false, phys, fire, cold, pois, nrgy, chaos, 0, false, false, false);
        }

        public static int Damage(Mobile m, Mobile from, int damage, bool ignoreArmor, int phys, int fire, int cold, int pois, int nrgy)
        {
            return Damage(m, from, damage, ignoreArmor, phys, fire, cold, pois, nrgy, 0, 0, false, false, false);
        }

        public static int Damage(Mobile m, Mobile from, int damage, bool ignoreArmor, int phys, int fire, int cold, int pois, int nrgy, int chaos, int direct, bool keepAlive, bool archer, bool deathStrike)
        {
            if (m == null || m.Deleted || !m.Alive || damage <= 0)
                return 0;

            if (phys == 0 && fire == 100 && cold == 0 && pois == 0 && nrgy == 0)
                Mobiles.MeerMage.StopEffect(m, true);

            m.Damage(damage, from);
            return damage;
        }

        public static int Scale(int input, int percent)
        {
            return input * percent / 100;
        }
    }

}
