using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlatforms : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
    {
		if (eventData.pointerDrag == null)
        {
			return;
		}
        else
        {
			CardsDrag _drag = eventData.pointerDrag.GetComponent<CardsDrag>();

			if (_drag != null)
            {
				_drag.cardDropZone = transform;
			}
		}
	}
	
	public void OnPointerExit(PointerEventData eventData)
    {
		if (eventData.pointerDrag == null)
        {
			return;
		}
        else
        {
			CardsDrag _drag = eventData.pointerDrag.GetComponent<CardsDrag>();

			if (_drag != null && _drag.cardDropZone == transform)
            {
				_drag.cardDropZone = _drag.yourHand;
			}
		}
	}
	
	public void OnDrop(PointerEventData eventData)
    {
		CardsDrag _drag = eventData.pointerDrag.GetComponent<CardsDrag>();
		GameObject _token = Instantiate(_drag.token, _drag.transform.position, _drag.transform.rotation) as GameObject;

		if (_drag != null)
        {
			if (_drag.yourHand != _drag.cardDropZone)
            {
				_token.transform.SetParent(transform);
				_token.transform.SetSiblingIndex(_drag.tokenHolder.transform.GetSiblingIndex());
				_token.transform.localScale = _token.transform.localScale / 2;
				_drag.selfDestruct = true;
			}
		}
	}
}