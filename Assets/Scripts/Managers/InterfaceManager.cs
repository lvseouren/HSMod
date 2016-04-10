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
    private Vector3 originPosition = Vector3.zero;

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
        interfaceParent.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        // Creating the GameObjects for each UI component
        arrowObject = CreateChildObject("Arrow", 0f);
        arrowCircleObject = CreateChildObject("ArrowCircle", 0f);
        arrowBodyObject = CreateChildObject("ArrowBody", 0f);

        // Creating the SpriteRenderer for each UI GameObject
        arrowRenderer = CreateChildSprite(arrowObject, "Sprites/UI/Arrow");
        arrowCircleRenderer = CreateChildSprite(arrowCircleObject, "Sprites/UI/ArrowCircle");
        arrowBodyRenderer = CreateChildSprite(arrowBodyObject, "Sprites/UI/ArrowBody");

        // Setting the sorting layer order
        arrowRenderer.sortingOrder = 2;
        arrowCircleRenderer.sortingOrder = 1;
        arrowBodyRenderer.sortingOrder = 0;
    }

    private void LateUpdate()
    {
        if (IsDragging)
        {
            // Arrow and Circle //

            // Getting the world position based on the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1000f));

            // Getting the vector direction from the minion to the mouse
            Vector3 arrowDirection = Input.mousePosition - originPosition;

            // Setting the arrow and the circle at the mouse position in the world space
            arrowRenderer.transform.position = mousePosition;
            arrowCircleRenderer.transform.position = mousePosition;

            // Getting the angle that forms between the minion and the mouse
            float arrowRotation = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;

            // Setting the arrow rotation 
            Vector3 rotation = new Vector3(0f, 0f, arrowRotation - 90);
            arrowRenderer.transform.localEulerAngles = rotation;


            // Arrow Body //

            // Setting the body of the arrow halfway between the mouse and the origin
            arrowBodyRenderer.transform.position = Camera.main.ScreenToWorldPoint(originPosition + (arrowDirection / 2) + new Vector3(0f, 0f, 1000f));

            // Setting the body rotation
            arrowBodyRenderer.transform.localEulerAngles = rotation;

            // Setting the arrow body scale based on the distance
            Vector3 worldArrowDirection = mousePosition - Camera.main.ScreenToWorldPoint(originPosition + new Vector3(0f, 0f, 1000f));
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

    private SpriteRenderer CreateChildSprite(GameObject rendererObject, string sprite)
    {
        SpriteRenderer spriteRenderer = rendererObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "UI";
        spriteRenderer.sprite = Resources.Load<Sprite>(sprite);
        spriteRenderer.enabled = false;

        return spriteRenderer;
    }

    public void EnableArrow()
    {
        originPosition = Input.mousePosition;

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