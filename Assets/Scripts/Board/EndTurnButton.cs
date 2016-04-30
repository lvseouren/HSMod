using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public bool IsEnabled = false;

    private Material ButtonMaterial;

    private Vector2 yellowPosition = new Vector2(0f, 0f);
    private Vector2 greenPosition = new Vector2(0f, 0.5f);
    private Vector2 greyPosition = new Vector2(0.5f, 0f);

    public void UpdateStatus(TurnButtonStatus status)
    {
        switch (status)
        {
            case TurnButtonStatus.Inactive:
                IsEnabled = false;
                ButtonMaterial.mainTextureOffset = greyPosition;
                break;

            case TurnButtonStatus.Active:
                IsEnabled = true;
                ButtonMaterial.mainTextureOffset = yellowPosition;
                break;

            case TurnButtonStatus.ActiveCompleted:
                IsEnabled = true;
                ButtonMaterial.mainTextureOffset = greenPosition;
                break;
        }
    }

    private void Start()
    {
        ButtonMaterial = this.GetComponent<MeshRenderer>().material;
    }

    private void OnMouseDown()
    {
        if (IsEnabled)
        {
            GameManager.Instance.TurnEnd();
        }
    }
}