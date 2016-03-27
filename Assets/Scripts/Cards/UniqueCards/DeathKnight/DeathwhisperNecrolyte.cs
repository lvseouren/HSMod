public class DeathwhisperNecrolyte : MinionCard
{
    public DeathwhisperNecrolyte()
    {
        Name = "Deathwhisper Necrolyte";
        Description = "Your other Undead have +1 Attack.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;
        MinionType = MinionType.Undead;

        Cost = 2;
        BaseAttack = 1;
        CurrentAttack = 1;
        Health = 4;
    }

    public override void OnDead()
    {
        // TODO : +1 Attack Aura
    }
}
