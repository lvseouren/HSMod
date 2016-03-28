public class DancingRuneblade : MinionCard
{
    public DancingRuneblade()
    {
        Name = "Dancing Runeblade";
        Description = "Battlecry: Gain Attack and Health equal to your weapon's Attack and Durability.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Rare;
        MinionType = MinionType.Undead;

        Cost = 1;
        BaseAttack = 1;
        CurrentAttack = 1;
        Health = 1;
    }

    public override void OnPlayed()
    {
        // TODO : Gain Attack and Health equal to your weapon's Attack and Durability
    }
}
