using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public Transform parentToReturnTo = null;

	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log("OnBeginDrag");
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	public void OnDrag(PointerEventData eventData){
		Debug.Log("Drag");
		this.transform.position = eventData.position;
	}
	public void OnEndDrag(PointerEventData eventData){
		Debug.Log("EndDrag");
		this.transform.SetParent (parentToReturnTo);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
