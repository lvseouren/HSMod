using UnityEngine;
using UnityEngine.UI;

public class StatsTextUpdate : MonoBehaviour {

	private Color normal = Color.white , boosted = Color.green , damaged = Color.red ;
	private Text front , back ;

	private Hero heroScript ;
	private BasicMinionSuperClass minion ;

	void Start () {
		front = transform.GetChild (1).GetComponent<Text> ();
		back = transform.GetChild (0).GetComponent<Text> ();

		heroScript = GetComponentInParent<Hero> ();
		minion = GetComponentInParent <BasicMinionSuperClass> ();
	}

	public void UpdateHealth () {
		int currentHp;
		int maxHp;
		if (heroScript) {
			currentHp = heroScript.currentHp;
			maxHp = heroScript.maximumHp;
		} else {
			currentHp = minion.currentHealth;
			maxHp = minion.baseHealth ;
		}
		if (currentHp == maxHp) {
			front.color = normal;
		} else if (currentHp < maxHp) {
			front.color = damaged;
		} else {
			front.color = boosted ;
		}
		front.text = currentHp.ToString ();
		back.text = currentHp.ToString ();
	}

	void Update () {
		UpdateHealth ();
	}

}
