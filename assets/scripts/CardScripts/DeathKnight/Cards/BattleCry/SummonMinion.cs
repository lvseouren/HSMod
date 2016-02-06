using UnityEngine;
using System.Collections;

public class SummonMinion : BattleCry {

	public GameObject[] MinionTokens;

	public override void OnBattleCry ()
	{
		base.OnBattleCry ();
		GameObject bf = GameObject.FindGameObjectWithTag ("MyBF" );
		foreach (GameObject m in MinionTokens) {
			if (bf.transform.childCount > 6 )
				return ;
			StartCoroutine (delaySummon (m , bf) );
		}
	}



	private IEnumerator delaySummon (GameObject m , GameObject bf) {

		yield return new WaitForSeconds (0.5f);

		GameObject instance  = (GameObject) Instantiate ( m , m.transform.position , m.transform.rotation );
		
		instance.transform.SetParent ( bf.transform );
		
		instance.transform.SetSiblingIndex ( transform.GetSiblingIndex () + 1 );
		bf.GetComponent<ArrangeChildren>().ArrangeCards () ;
	}




}
