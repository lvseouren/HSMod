using UnityEngine;

public class Hero : MonoBehaviour, ICharacter
{
    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Armor = 0;

    // Effects //
    public bool Frozen = false;
    public bool Immune = false;
    public bool Forgetful = false;

    private void Start()
    {

    }

    public void Attack(ICharacter target)
    {
        
    }

    public void Damage(int damageAmount)
    {

    }
}