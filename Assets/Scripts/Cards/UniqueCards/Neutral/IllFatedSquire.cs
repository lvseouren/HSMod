using System;
using System.Collections.Generic;

public class IllFatedSquire : MinionCard
{
    public IllFatedSquire()
    {
        Name = "Ill-Fated Squire";
        Description = "Deathrattle: Put a random weapon from your deck into your hand.";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Rare;
        MinionType = MinionType.Undead;

        BaseCost = 2;
        BaseAttack = 2;
        BaseHealth = 1;

        Buffs.Deathrattle.Subscribe(x => Deathrattle(x));

        InitializeMinion();
    }

    public void Deathrattle(Minion minion)
    {
        if (minion.Player.Deck.ContainsCardOfType<WeaponCard>())
        {
            List<BaseCard> weaponsInDeck = minion.Player.Deck.GetCardsOfType<WeaponCard>();

            if (weaponsInDeck.Count > 0)
            {
                BaseCard randomWeapon = RNG.RandomCard(weaponsInDeck);

                minion.Player.DrawFromDeck(randomWeapon);
            }
        }
    }
}