using UnityEngine;

public class Hero : MonoBehaviour, IDamageable
{
    // Stats //
    public int Attack = 0;
    public int BaseHP = 30;
    public int CurrentHp;
    public int Armor = 0;

    // Effects //
    public bool Frozen = false;
    public bool Immune = false;
    public bool Forgetful = false;

    private void Start()
    {

    }

    public void Damage(int damageAmount)
    {

    }
}