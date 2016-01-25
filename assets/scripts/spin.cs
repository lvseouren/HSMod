using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {
	
	public float angAmt ;
	bool comecou = false ;
	
	public IEnumerator fliping () {
		if (comecou)
			yield break;

		comecou = true;
		float initialAngle = 0f;

		while (initialAngle < 180){
			transform.Rotate(new Vector3 (0 , 0 , -1 ) , angAmt);
			initialAngle += angAmt;
			yield return null;
		}
		comecou = false;

	}




}