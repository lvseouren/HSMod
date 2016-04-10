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
        _instance = this;

        interfaceParent = new GameObject("InterfaceParent");
        interfaceParent.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        arrowObject = CreateChildObject("Arrow", 0f);
        arrowCircleObject = CreateChildObject("ArrowCircle", 0f);
        arrowBodyObject = CreateChildObject("ArrowBody", 0f);

        arrowRenderer = CreateChildSprite(arrowObject, "Sprites/UI/Arrow");
        arrowCircleRenderer = CreateChildSprite(arrowCircleObject, "Sprites/UI/ArrowCircle");
        arrowBodyRenderer = CreateChildSprite(arrowBodyObject, "Sprites/UI/ArrowBody");
    }

    private void LateUpdate()
    {
        if (IsDragging)
        {
            // Getting the world position based on the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1000f));

            // Getting the vector direction from the minion to the mouse
            Vector3 arrowDirection = Input.mousePosition - originPosition;

            // Setting the arrow and the circle at the mouse position in the world space
            arrowRenderer.transform.position = mousePosition;
            arrowCircleRenderer.transform.position = mousePosition;

            // Setting the body of the arrow halfway between the mouse and the origin
            arrowBodyRenderer.transform.position = Camera.main.ScreenToWorldPoint(originPosition + (arrowDirection / 2) + new Vector3(0f, 0f, 1000f));

            // Getting the angle that forms between the minion and the mouse
            float arrowRotation = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;

            // Setting the arrow rotation 
            arrowRenderer.transform.localEulerAngles = new Vector3(0f, 0f, arrowRotation - 90);
            arrowBodyRenderer.transform.localEulerAngles = new Vector3(0f, 0f, arrowRotation - 90);

            // Setting the arrow body scale based on the distance
            // TODO : Fix for all resolutions
            arrowBodyRenderer.transform.localScale = new Vector3(80f, Mathf.Log10(Mathf.Pow(arrowDirection.magnitude, 0.15f) + 0.001f) * arrowDirection.magnitude, 80f);

            // TODO : Set the arrow body offset (animate arrow
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