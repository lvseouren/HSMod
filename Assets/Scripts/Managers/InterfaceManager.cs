using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    // Singleton //
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
    
    // Control Fields //
    [HideInInspector] public bool IsDragging;
    private Vector3 screenOriginPosition = Vector3.zero;

    // Sprite GameObjects //
    private GameObject interfaceParent;
    private GameObject arrowObject;
    private GameObject arrowCircleObject;
    private GameObject arrowBodyObject;

    // Sprite Renderers //
    private SpriteRenderer arrowRenderer;
    private SpriteRenderer arrowCircleRenderer;
    private SpriteRenderer arrowBodyRenderer;

    private void Start()
    {
        // Setting the Singleton Instance
        _instance = this;

        // Creating the parent GameObject for the UI
        interfaceParent = new GameObject("InterfaceParent");

        // Creating the GameObjects for each UI component
        arrowObject = CreateChildObject("Arrow", 0f);
        arrowCircleObject = CreateChildObject("ArrowCircle", 0f);
        arrowBodyObject = CreateChildObject("ArrowBody", 0f);

        // Creating the SpriteRenderer for each UI GameObject
        arrowCircleRenderer = CreateChildSprite(arrowCircleObject, "Sprites/UI/ArrowCircle", 50);
        arrowBodyRenderer = CreateChildSprite(arrowBodyObject, "Sprites/UI/ArrowBody", 51);
        arrowRenderer = CreateChildSprite(arrowObject, "Sprites/UI/Arrow", 52);
    }

    private void LateUpdate()
    {
        if (IsDragging)
        {
            // Arrow and Circle //

            // Getting the world position based on the mouse position
            Vector3 gameMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1000f));

            // Getting the vector direction from the minion to the mouse
            Vector3 screenMouseDirection = Input.mousePosition - screenOriginPosition;

            // Setting the arrow and the circle at the mouse position in the world space
            arrowRenderer.transform.position = gameMousePosition;
            arrowCircleRenderer.transform.position = gameMousePosition;

            // Getting the angle that forms between the minion and the mouse
            float arrowRotationAngle = Mathf.Atan2(screenMouseDirection.y, screenMouseDirection.x) * Mathf.Rad2Deg - 90;

            // Setting the arrow rotation 
            Vector3 arrowRotation = new Vector3(90f, 0f, arrowRotationAngle);
            arrowRenderer.transform.localEulerAngles = arrowRotation;

            // Arrow Body //

            // Setting the body of the arrow halfway between the mouse and the origin
            Vector3 bodyPosition = Camera.main.ScreenToWorldPoint(screenOriginPosition + (screenMouseDirection / 2f) + new Vector3(0f, 0f, 1000f));
            arrowBodyRenderer.transform.localPosition = new Vector3(bodyPosition.x, 100f, bodyPosition.z);

            // Setting the body rotation
            arrowBodyRenderer.transform.localEulerAngles = arrowRotation;

            // Setting the arrow body scale based on the distance
            Vector3 worldArrowDirection = gameMousePosition - Camera.main.ScreenToWorldPoint(screenOriginPosition + new Vector3(0f, 0f, 1000f));
            float worldArrowScale = 0.27f * worldArrowDirection.magnitude;
            arrowBodyRenderer.transform.localScale = new Vector3(80f, worldArrowScale, 80f);

            // TODO : Set the arrow body offset (animate arrow)
        }
    }

    private GameObject CreateChildObject(string name, float position)
    {
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = this.interfaceParent.transform;
        glowObject.transform.localPosition = new Vector3(0f, 0f, position);
        glowObject.transform.localEulerAngles = Vector3.zero;
        glowObject.transform.localScale = new Vector3(80f, 80f, 80f);

        return glowObject;
    }

    private SpriteRenderer CreateChildSprite(GameObject rendererObject, string sprite, int order)
    {
        SpriteRenderer spriteRenderer = rendererObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = order;
        spriteRenderer.sprite = Resources.Load<Sprite>(sprite);
        spriteRenderer.enabled = false;

        return spriteRenderer;
    }

    public void EnableArrow(Vector3 origin)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(origin);

        screenOriginPosition = new Vector3(screenPosition.x, screenPosition.y, 0f);

        IsDragging = true;

        arrowRenderer.enabled = true;
        arrowBodyRenderer.enabled = true;
    }

    public void DisableArrow()
    {
        IsDragging = false;

        arrowRenderer.enabled = false;
        arrowBodyRenderer.enabled = false;

        DisableArrowCircle();
    }

    public void EnableArrowCircle()
    {
        arrowCircleRenderer.enabled = true;
    }

    public void DisableArrowCircle()
    {
        arrowCircleRenderer.enabled = false;
    }
}