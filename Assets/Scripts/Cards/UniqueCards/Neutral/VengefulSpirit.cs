public class VengefulSpirit : MinionCard
{
    public VengefulSpirit()
    {
        Name = "Vengeful Spirit";
        Description = "Deathrattle: Summon a 2/1 Spiteful Wraith.";

        CardClass = CardClass.Neutral;
        Rarity = Rarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 2;
        BaseAttack = 2;
        CurrentAttack = 2;
        BaseHealth = 2;
    }

    public override void OnDead()
    {
        // TODO : Spawn 2/1 Spiteful Wrath
    }
}