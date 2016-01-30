using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class CCUIManager : MonoBehaviour {
    //basic card info
    Toggle creatureToggle;
    Toggle spellToggle;
    Toggle weaponToggle;
    Toggle tokenToggle;

    string cardName;

    int manaCost;

    GameObject creatureInfo;
    GameObject spellInfo;
    GameObject weaponInfo;
    GameObject tokenInfo;

    void Reset()
    {
        
    }
    
    // Use this for initialization
    void Start ()
    {
        creatureInfo = GameObject.Find("CreatureInfoText");
        creatureInfo.SetActive(false);
        spellInfo = GameObject.Find("SpellInfoText");
        spellInfo.SetActive(false);
        weaponInfo = GameObject.Find("WeaponInfoText");
        weaponInfo.SetActive(false);
        tokenInfo = GameObject.Find("TokenInfoText");
        tokenInfo.SetActive(false);
        creatureToggle = GameObject.Find("CreatureToggle").GetComponent<Toggle>();
        creatureToggle.onValueChanged.AddListener(togglingCreatureType);
        spellToggle = GameObject.Find("SpellToggle").GetComponent<Toggle>();
        spellToggle.onValueChanged.AddListener(togglingSpellType);
        weaponToggle = GameObject.Find("WeaponToggle").GetComponent<Toggle>();
        weaponToggle.onValueChanged.AddListener(togglingWeaponType);
        tokenToggle = GameObject.Find("TokenToggle").GetComponent<Toggle>();
        tokenToggle.onValueChanged.AddListener(togglingTokenType);
    }

    public void togglingCreatureType(bool value)
    {
        Debug.Log(creatureInfo +""+ value);
        Debug.Log(creatureInfo.gameObject.activeSelf);
        
        if (value)
        {
            creatureInfo.SetActive(true);
            spellInfo.SetActive(false);
            spellToggle.isOn = false;
            weaponInfo.SetActive(false);
            weaponToggle.isOn = false;
            tokenInfo.SetActive(false);
            tokenToggle.isOn = false;
        }
        else
            creatureInfo.SetActive(false);
        
    }
    public void togglingSpellType(bool value)
    {

        if (value)
        {
            creatureInfo.SetActive(false);
            creatureToggle.isOn = false;
            spellInfo.SetActive(true);
            weaponInfo.SetActive(false);
            weaponToggle.isOn = false;
            tokenInfo.SetActive(false);
            tokenToggle.isOn = false;
        }
        else
            spellInfo.SetActive(false);
    }
    public void togglingWeaponType(bool value)
    {
        if (value)
        {
            creatureInfo.SetActive(false);
            creatureToggle.isOn = false;
            spellInfo.SetActive(false);
            spellToggle.isOn = false;
            weaponInfo.SetActive(true);
            tokenInfo.SetActive(false);
            tokenToggle.isOn = false;
        }
        else
            weaponInfo.SetActive(false);
    }
    public void togglingTokenType(bool value)
    {
        if (value)
        {
            creatureInfo.SetActive(false);
            creatureToggle.isOn = false;
            spellInfo.SetActive(false);
            spellToggle.isOn = false;
            weaponInfo.SetActive(false);
            weaponToggle.isOn = false;
            tokenInfo.SetActive(true);
        }
        else
            tokenInfo.SetActive(false);
    }

}
