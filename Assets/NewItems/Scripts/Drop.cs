using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("Pointer enter");
	}

	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("Pointer exit");
	}

	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name +"was Dropped on"+gameObject.name);
		Drag d = eventData.pointerDrag.GetComponent<Drag> ();

		if(d!=null){
			d.parentToReturnTo = this.transform;
		}
	}

}
