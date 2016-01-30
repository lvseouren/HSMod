using UnityEngine;
using System.Collections;

public class ArrangeChildren : MonoBehaviour {


	public float zOffset, yOffset, angleVariation, xDistance , powFactor;

	public float zRotation, yRotation, xRotation ;


	public IEnumerator delayArrange () {
		yield return null;
		ArrangeCards ();
	}

	void Start () {
		ArrangeCardsInstant ();
	}

	void Update  () {
		if (Input.GetKeyDown (KeyCode.T)) {
			ArrangeCards ();
		}
	}

	public void ArrangeCardsInstant () {
		
		
		float metadeDaMao = ( transform.childCount + 1 )/2f;
		
		int i = 1;
		foreach (Transform t in transform) {
			float dif = i - metadeDaMao;
			
			Vector3 temp = t.localPosition;
			temp.z = - Mathf.Pow ((Mathf.Abs (dif)), powFactor) * zOffset;
			temp.y = i * yOffset;
			temp.x = dif * xDistance  ;
			
			// lerp the position
			t.transform.localPosition =  temp;
			
			temp = t.eulerAngles;
			//	temp.x = 0;
			//	temp.z = 0 ;
			
			float rotation = dif * angleVariation;
			temp.x = xRotation ;
			temp.z = zRotation ;
			
			if (yRotation == -1 ){
				temp.y =  (rotation);
			}else {
				temp.y = yRotation;
			}
			// lerp the rotation
			t.eulerAngles = temp;
			i++;
		}
	}

	public void ArrangeCards () {



		float metadeDaMao = ( transform.childCount + 1 )/2f;

		int i = 1;
		foreach (Transform t in transform) {

			float dif = i - metadeDaMao;

			Vector3 temp = t.localPosition;
			temp.z = - Mathf.Pow ((Mathf.Abs (dif)), powFactor) * zOffset;
			temp.y = i * yOffset;
			temp.x = dif * xDistance  ;

			// lerp the position
			if (!t.GetComponent <ArrangeChildLerper>().isAlreadyLerping && t.gameObject.activeInHierarchy ){
				t.GetComponent<ArrangeChildLerper>().StartCoroutine ( t.GetComponent<ArrangeChildLerper>().
				                                                     lerpPositionsOverTime  (temp) );
			}else if (t.gameObject.activeInHierarchy) {
				t.GetComponent<ArrangeChildLerper>().targetPosition = temp ;

			}
			temp = t.eulerAngles;
			float rotation = dif * angleVariation;
			temp.x = xRotation ;
			temp.z = zRotation ;

			if (yRotation == -1 ){
				temp.y =  (rotation);
			}else {
				temp.y = yRotation;
			}
			t.eulerAngles = temp;
			i++;
		}
	}
}
