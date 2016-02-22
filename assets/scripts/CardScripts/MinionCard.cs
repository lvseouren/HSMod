using UnityEngine;
using System.Collections;

public class MinionCard : MonoBehaviour {

	public GameObject[] token ;
	public int manaCost ;
	public bool hasTargetBattlecry ;

	public virtual void OnPlay (int index) {
		GameObject mybf = GameObject.FindGameObjectWithTag ("MyBF");


		foreach (GameObject t in token) {
			GameObject instance = Instantiate (t);
			mybf.GetComponent<InsertChild>().insertChildInPosition ( index , instance );
			//instance.GetComponent<BasicMinionSuperClass>().OnSummon () ;
			//instance.GetComponent<BasicMinionSuperClass>().OnBattleCry ();

		}




	}


}