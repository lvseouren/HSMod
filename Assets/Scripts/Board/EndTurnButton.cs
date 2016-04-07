using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    private bool _isEnabled;

    private void OnMouseDown()
    {
        if (_isEnabled)
        {
            GameManager.Instance.TurnEnd();
        }
    }
}