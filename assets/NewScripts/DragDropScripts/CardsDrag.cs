using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CardsDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform yourHand = null;
	public Transform cardDropZone = null;

	public GameObject token;
	public GameObject tokenHolder = null;

	public bool selfDestruct = false;
	
	public void OnBeginDrag(PointerEventData eventData) {
		tokenHolder = new GameObject("PlaceHolder");
		tokenHolder.transform.SetParent( transform.parent );
		tokenHolder.transform.SetSiblingIndex( transform.GetSiblingIndex() );

		LayoutElement _layout = tokenHolder.AddComponent<LayoutElement>();
		_layout.preferredWidth = GetComponent<LayoutElement>().preferredWidth;

		yourHand = transform.parent;
		cardDropZone = yourHand;
		transform.SetParent( transform.parent.parent );
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
		
	public void OnDrag(PointerEventData eventData) {

		transform.position = eventData.position;

		if(tokenHolder.transform.parent != cardDropZone)
			tokenHolder.transform.SetParent(cardDropZone);

		int _newSiblingIndex = cardDropZone.childCount;

		for(int i=0; i < cardDropZone.childCount; i++) {
			if(transform.position.x < cardDropZone.GetChild(i).position.x) {

				_newSiblingIndex = i;

				if(tokenHolder.transform.GetSiblingIndex() < _newSiblingIndex)
					_newSiblingIndex--;

				break;
			}
		}

		tokenHolder.transform.SetSiblingIndex(_newSiblingIndex);

	}
	
	public void OnEndDrag(PointerEventData eventData) {
		transform.SetParent( yourHand );
		transform.SetSiblingIndex( tokenHolder.transform.GetSiblingIndex() );

		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(tokenHolder);
		if(selfDestruct) {
			Destroy (gameObject);
		}
	}
}
