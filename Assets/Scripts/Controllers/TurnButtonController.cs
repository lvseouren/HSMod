using UnityEngine;

public class TurnButtonController : MonoBehaviour
{
    public bool IsEnabled = true;

    private Animator Animator;
    private Material ButtonMaterial;

    private TurnButtonStatus Status;

    private Vector3 BasePosition = new Vector3(1475f, 45f, 627.5f);
    private Vector3 PressedPosition = new Vector3(1475f, 25f, 627.5f);
    private Vector3 TargetPosition;

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
        ButtonMaterial = GetComponent<MeshRenderer>().material;
        TargetPosition = BasePosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 5f);
    }

    private void OnMouseDown()
    {
        if (IsEnabled)
        {
            TargetPosition = PressedPosition;

            GameManager.Instance.TurnEnd();
        }
    }

    private void OnMouseUp()
    {
        if (IsEnabled)
        {
            TargetPosition = BasePosition;

        }
    }
}