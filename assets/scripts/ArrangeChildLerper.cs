using UnityEngine;
using System.Collections;

public class ArrangeChildLerper : MonoBehaviour {

	public float speed ; // in seconds 
	public float distanceOffset ;

	public bool isAlreadyLerping = false ;
	public Vector3 targetPosition ;


	public IEnumerator lerpPositionsOverTime ( Vector3 FinalPosition) {

		isAlreadyLerping = true;
		targetPosition = FinalPosition;

		while (Vector3.Distance ( transform.localPosition , targetPosition ) > distanceOffset ) {

			transform.localPosition = Vector3.Lerp ( transform.localPosition , targetPosition , speed) ;
			yield return null;
		}
		transform.localPosition = targetPosition;
		isAlreadyLerping = false;
	}

}