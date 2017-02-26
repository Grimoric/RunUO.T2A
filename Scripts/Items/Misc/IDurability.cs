namespace Server.Items
{
    interface IDurability
	{
		bool CanFortify { get; }

		int InitMinHits { get; }
		int InitMaxHits { get; }

		int HitPoints { get; set; }
		int MaxHitPoints { get; set; }
	}

	interface IWearableDurability : IDurability
	{
		int OnHit( BaseWeapon weapon, int damageTaken );
	}
}