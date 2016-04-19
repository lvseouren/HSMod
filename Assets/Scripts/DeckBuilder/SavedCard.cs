using System;

[Serializable]
public class SavedCard
{
    public string Name;
    public bool Golden;

    public SavedCard(string name, bool golden)
    {
        Name = name;
        Golden = golden;
    }

    public BaseCard ToGameCard()
    {
        // Getting the Type of the card based on the name
        Type cardType = Type.GetType(Name);

        if (cardType != null)
        {
            return (BaseCard) Activator.CreateInstance(cardType);
        }

        return null;
    }
}