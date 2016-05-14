using UnityEngine;

public class TurnButtonController : MonoBehaviour
{
    public bool IsEnabled = false;

    private Animator Animator;
    private Material ButtonMaterial;

    private Vector2 yellowPosition = new Vector2(0f, 0f);
    private Vector2 greenPosition = new Vector2(0f, 0.5f);
    private Vector2 greyPosition = new Vector2(0.5f, 0f);

    public static TurnButtonController Create()
    {
        GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs/Button_EndTurn");
        GameObject turnButton = Instantiate(buttonPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;

        TurnButtonController controller = turnButton.AddComponent<TurnButtonController>();

        return controller;
    }

    private void Start()
    {
        ButtonMaterial = GetComponent<MeshRenderer>().material;
    }

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

    private void OnMouseDown()
    {
        if (IsEnabled)
        {
            GameManager.Instance.TurnEnd();

            UpdateStatus(TurnButtonStatus.Inactive);
        }
    }
}