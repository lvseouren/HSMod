using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public bool IsEnabled;

    private void OnMouseDown()
    {
        if (IsEnabled)
        {
            GameManager.Instance.TurnEnd();
        }
    }
}