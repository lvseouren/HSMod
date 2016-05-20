using System.Collections;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    #region Singleton

    private static InterfaceManager _instance;

    public static InterfaceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InterfaceManager();
            }
            return _instance;
        }
    }

    private InterfaceManager() { }

    #endregion

    // Control Fields //
    public bool IsTargeting;
    public bool IsDragging;
    private BaseController originController;
    private Vector3 worldOriginPosition = Vector3.zero;

    // Sprite Renderers //
    private SpriteRenderer turnRenderer;
    private SpriteRenderer arrowRenderer;
    private SpriteRenderer circleRenderer;
    private SpriteRenderer bodyRenderer;

    private IEnumerator TurnFadeCoroutine;

    private void Start()
    {
        // Setting the singleton instance
        _instance = this;

        // Creating the SpriteRenderer for each UI GameObject
        turnRenderer = CreateChildSprite("Sprites/General/YourTurn", 1003);
        arrowRenderer = CreateChildSprite("Sprites/UI/Arrow", 1002);
        bodyRenderer = CreateChildSprite("Sprites/UI/ArrowBody", 1001);
        circleRenderer = CreateChildSprite("Sprites/UI/ArrowCircle", 1000);

        turnRenderer.transform.localPosition = new Vector3(10f, 0f, 8f);
        turnRenderer.transform.localEulerAngles = Vector3.right * 90f;
    }

    private void LateUpdate()
    {
        if (IsTargeting)
        {
            // Getting the world position of the mouse
            Vector3 worldMousePosition = Util.GetWorldMousePosition();

            // Calculating the direction vector from the minion to the mouse
            Vector3 directionVector = worldMousePosition - worldOriginPosition;

            // Getting the angle that forms between the minion and the mouse
            float directionAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;

            // Calculating the direction rotation
            Vector3 directionRotation = new Vector3(90f, directionAngle, 0f);
            
            #region Arrow and Circle

            arrowRenderer.transform.position = worldMousePosition;
            arrowRenderer.transform.localEulerAngles = directionRotation;

            circleRenderer.transform.position = worldMousePosition;
            circleRenderer.transform.localEulerAngles = Vector3.right * 90f;

            #endregion

            #region Body

            // Calculating the position halfway between the mouse and the origin
            Vector3 bodyPosition = worldOriginPosition + directionVector / 2f;

            // Calculating the body scale based on the distance between the mouse and the origin
            float bodyScale = 0.003375f * directionVector.magnitude;

            bodyRenderer.transform.position = new Vector3(bodyPosition.x, 100f, bodyPosition.z);
            bodyRenderer.transform.localEulerAngles = directionRotation;
            bodyRenderer.transform.localScale = new Vector3(1f, bodyScale, 1f);

            // TODO : Animate the arrow

            #endregion
        }
    }

    private SpriteRenderer CreateChildSprite(string sprite, int order)
    {
        GameObject rendererObject = new GameObject(sprite.Substring(sprite.LastIndexOf("/") + 1));
        rendererObject.transform.ChangeParent(this.transform);

        SpriteRenderer spriteRenderer = rendererObject.AddComponent<SpriteRenderer>();
        spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = order;
        spriteRenderer.sprite = Resources.Load<Sprite>(sprite);
        spriteRenderer.enabled = false;

        return spriteRenderer;
    }

    public void EnableArrowAt(BaseController controller, Vector3 position)
    {
        worldOriginPosition = position;
        originController = controller;

        IsTargeting = true;

        arrowRenderer.enabled = true;
        bodyRenderer.enabled = true;
    }

    public void EnableArrow(BaseController controller)
    {
        EnableArrowAt(controller, controller.transform.position);
    }

    public void DisableArrow()
    {
        IsTargeting = false;

        arrowRenderer.enabled = false;
        bodyRenderer.enabled = false;

        DisableArrowCircle();
    }

    public void OnHoverStart(BaseController controller)
    {
        if (IsTargeting && controller != originController)
        {
            circleRenderer.enabled = true;
        }
    }

    public void OnHoverStop()
    {
        circleRenderer.enabled = false;
    }

    public void EnableArrowCircle()
    {
        circleRenderer.enabled = true;
    }

    public void DisableArrowCircle()
    {
        circleRenderer.enabled = false;
    }

    public void SpawnTurnSprite()
    {
        if (TurnFadeCoroutine != null)
        {
            StopCoroutine(TurnFadeCoroutine);
        }

        TurnFadeCoroutine = TurnSpriteFade();
        StartCoroutine(TurnFadeCoroutine);
    }

    private IEnumerator TurnSpriteFade()
    {
        turnRenderer.enabled = true;
        turnRenderer.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1f);

        for (float i = 1f; i > 0f; i -= 0.02f)
        {
            turnRenderer.transform.localScale = Vector3.one * i;
            yield return null;
        }

        turnRenderer.enabled = false;
    }
}