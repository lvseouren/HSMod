using UnityEngine;
using System.Collections;

public class EndTurnButton : MonoBehaviour {

    void OnMouseDown()
    {
        GameManager.Instance.TurnEnd();
    }
}
