public class DeathwhisperNecrolyte : MinionCard
{
    public DeathwhisperNecrolyte()
    {
        Name = "Deathwhisper Necrolyte";
        Description = "Your other Undead have +1 Attack.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 2;
        BaseAttack = 1;
        BaseHealth = 4;

        InitializeMinion();
    }

    public void Aura()
    {
        // TODO : Your other Undead have +1 Attack
    }
}