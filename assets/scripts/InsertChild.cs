using UnityEngine;
using System.Collections;

public class InsertChild : MonoBehaviour {

	public void insertChildInPosition  (int position , GameObject newChild) {

		if (position > transform.childCount) {
			Debug.LogError ("Out of bounds");
			return ;
		}


		newChild.transform.SetParent (transform);
//		for (int i = position+1; i < transform.childCount; i ++) {
//			transform.GetChild(i).SetSiblingIndex ( i+1 );
//		}
		newChild.transform.SetSiblingIndex (position);

		//GetComponent<ArrangeChildren> ().ArrangeCards ();
	}


}