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

    // Sprite GameObjects //
    private GameObject arrowObject;
    private GameObject circleObject;
    private GameObject bodyObject;

    // Sprite Renderers //
    private SpriteRenderer arrowRenderer;
    private SpriteRenderer circleRenderer;
    private SpriteRenderer bodyRenderer;

    private void Start()
    {
        // Setting the singleton instance
        _instance = this;

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

            #endregion

            #region Body

            // Calculating the position halfway between the mouse and the origin
            Vector3 bodyPosition = worldOriginPosition + directionVector / 2f;

            // Calculating the body scale based on the distance between the mouse and the origin
            float bodyScale = 0.27f * directionVector.magnitude;

            bodyRenderer.transform.localPosition = new Vector3(bodyPosition.x, 100f, bodyPosition.z);
            bodyRenderer.transform.localEulerAngles = directionRotation;
            bodyRenderer.transform.localScale = new Vector3(80f, bodyScale, 80f);

            // TODO : Animate the arrow

            #endregion
        }
    }

    private GameObject CreateChildObject(string name, float position)
    {
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = this.transform;
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
}