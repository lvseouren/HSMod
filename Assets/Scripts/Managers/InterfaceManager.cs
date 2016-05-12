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
    private bool IsDragging;
    private BaseController originController;
    private Vector3 screenOriginPosition = Vector3.zero;

    // Sprite GameObjects //
    private GameObject interfaceParent;
    private GameObject arrowObject;
    private GameObject circleObject;
    private GameObject bodyObject;

    // Sprite Renderers //
    private SpriteRenderer arrowRenderer;
    private SpriteRenderer circleRenderer;
    private SpriteRenderer bodyRenderer;

    private void Start()
    {
        // Setting the Singleton Instance
        _instance = this;

        // Creating the parent GameObject for the UI
        interfaceParent = new GameObject("InterfaceParent");

        // Creating the GameObjects for each UI component
        arrowObject = CreateChildObject("Arrow", 0f);
        circleObject = CreateChildObject("ArrowCircle", 0f);
        bodyObject = CreateChildObject("ArrowBody", 0f);

        // Creating the SpriteRenderer for each UI GameObject
        arrowRenderer = CreateChildSprite(arrowObject, "Sprites/UI/Arrow", 1002);
        bodyRenderer = CreateChildSprite(bodyObject, "Sprites/UI/ArrowBody", 1001);
        circleRenderer = CreateChildSprite(circleObject, "Sprites/UI/ArrowCircle", 1000);
    }

    private void LateUpdate()
    {
        if (IsDragging)
        {
            // Getting the world position of the mouse
            Vector3 worldMousePosition = Util.GetWorldMousePosition();
            Vector3 worldNormalizedPosition = new Vector3(worldMousePosition.x, 100f, worldMousePosition.z);

            // Getting the direction vector from the minion to the mouse
            Vector3 directionVector = Input.mousePosition - screenOriginPosition;

            // Getting the angle that forms between the minion and the mouse
            float directionAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg - 90;

            // Setting the arrow rotation 
            Vector3 directionRotation = new Vector3(90f, 0f, directionAngle);


            // Arrow //

            arrowRenderer.transform.position = worldNormalizedPosition;
            arrowRenderer.transform.localEulerAngles = directionRotation;

            
            // Circle //

            circleRenderer.transform.position = worldNormalizedPosition;


            // Arrow Body //

            // Getting the position halfway between the mouse and the origin
            Vector3 bodyPosition = Camera.main.ScreenToWorldPoint(screenOriginPosition + (directionVector / 2f) + new Vector3(0f, 0f, 1000f));

            // Getting the body scale based on the distance between the mouse and the origin
            Vector3 worldArrowDirection = worldMousePosition - Camera.main.ScreenToWorldPoint(screenOriginPosition + new Vector3(0f, 0f, 1000f));
            float worldArrowScale = 0.27f * worldArrowDirection.magnitude;

            bodyRenderer.transform.localPosition = new Vector3(bodyPosition.x, 100f, bodyPosition.z);
            bodyRenderer.transform.localEulerAngles = directionRotation;
            bodyRenderer.transform.localScale = new Vector3(80f, worldArrowScale, 80f);

            // TODO : Animate the arrow
        }
    }

    private GameObject CreateChildObject(string name, float position)
    {
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = interfaceParent.transform;
        glowObject.transform.localPosition = new Vector3(0f, 0f, position);
        glowObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        glowObject.transform.localScale = new Vector3(80f, 80f, 80f);

        return glowObject;
    }

    private SpriteRenderer CreateChildSprite(GameObject rendererObject, string sprite, int order)
    {
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
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

        screenOriginPosition = new Vector3(screenPosition.x, screenPosition.y, 0f);
        originController = controller;

        IsDragging = true;

        arrowRenderer.enabled = true;
        bodyRenderer.enabled = true;
    }

    public void EnableArrow(BaseController controller)
    {
        EnableArrowAt(controller, controller.transform.position);
    }

    public void DisableArrow()
    {
        IsDragging = false;

        arrowRenderer.enabled = false;
        bodyRenderer.enabled = false;

        DisableArrowCircle();
    }

    public void OnHoverStart(BaseController controller)
    {
        if (IsDragging && controller != originController)
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
}