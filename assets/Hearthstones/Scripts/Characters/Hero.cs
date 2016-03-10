using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	public int maximumHp = 30 ;
	public int currentHp ;
	public int attack = 0 ;
    public int armor = 0;

	public bool isFrozen;
	public bool hasWindfury;
	public bool isInvulnerable;
    public bool hasSpellImmunity;

    // later can make other functions like this for other battle modes
    // unless we don't make brawl etc
    void StandardInitialization(){
        armor = 0;
        maximumHp = 30;
        attack = 0;
        isFrozen = false;
        hasWindfury = false;
        isInvulnerable = false;
    }

	// Use this for initialization
	void Start () {
        StandardInitialization();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
