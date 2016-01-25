using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CCUIManeger : MonoBehaviour {
    //basic card info
    Toggle creaturetoggle;
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
        Debug.Log("fuckdis");
        creatureInfo = GameObject.Find("CreatureInfoText");
        creatureInfo.SetActive(false);
        spellInfo = GameObject.Find("SpellInfoText");
        spellInfo.SetActive(false);
        weaponInfo = GameObject.Find("WeaponInfoText");
        weaponInfo.SetActive(false);
        tokenInfo = GameObject.Find("TokenInfoText");
        tokenInfo.SetActive(false);
        creaturetoggle = GameObject.Find("CreatureToggle").GetComponent<Toggle>();
        creaturetoggle.onValueChanged.AddListener(toggelingCreatureType);
        spellToggle = GameObject.Find("SpellToggle").GetComponent<Toggle>();
        spellToggle.onValueChanged.AddListener(toggelingSpellType);
        weaponToggle = GameObject.Find("WeaponToggle").GetComponent<Toggle>();
        weaponToggle.onValueChanged.AddListener(toggelingWeaponType);
        tokenToggle = GameObject.Find("TokenToggle").GetComponent<Toggle>();
        tokenToggle.onValueChanged.AddListener(toggelingTokenType);



    }
	
    public void toggelingCreatureType(bool value)
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
    public void toggelingSpellType(bool value)
    {

        if (value)
        {
            creatureInfo.SetActive(false);
            creaturetoggle.isOn = false;
            spellInfo.SetActive(true);
            weaponInfo.SetActive(false);
            weaponToggle.isOn = false;
            tokenInfo.SetActive(false);
            tokenToggle.isOn = false;
        }
        else
            spellInfo.SetActive(false);
    }
    public void toggelingWeaponType(bool value)
    {
        if (value)
        {
            creatureInfo.SetActive(false);
            creaturetoggle.isOn = false;
            spellInfo.SetActive(false);
            spellToggle.isOn = false;
            weaponInfo.SetActive(true);
            tokenInfo.SetActive(false);
            tokenToggle.isOn = false;
        }
        else
            weaponInfo.SetActive(false);
    }
    public void toggelingTokenType(bool value)
    {
        if (value)
        {
            creatureInfo.SetActive(false);
            creaturetoggle.isOn = false;
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
