public class DeathwhisperNecrolyte : MinionCard
{
    public DeathwhisperNecrolyte()
    {
        Name = "Deathwhisper Necrolyte";
        Description = "Your other Undead have +1 Attack.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 2;
        BaseAttack = 1;
        BaseHealth = 4;
    }

    public void Aura()
    {
        // TODO : our other Undead have +1 Attack
    }
}