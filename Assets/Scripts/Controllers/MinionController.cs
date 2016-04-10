using UnityEngine;

public class MinionController : MonoBehaviour
{
    private SpriteRenderer greenGlowRenderer;
    private SpriteRenderer whiteGlowRenderer;
    private SpriteRenderer redGlowRenderer;

    private void Start()
    {
        greenGlowRenderer = CreateChildSprite("GreenGlow", "Sprites/Glows/Minion_Normal_GreenGlow", 0.01f);
        whiteGlowRenderer = CreateChildSprite("WhiteGlow", "Sprites/Glows/Minion_Normal_WhiteGlow", 0.01f);
        redGlowRenderer = CreateChildSprite("RedGlow", "Sprites/Glows/Minion_Normal_RedGlow", 0.01f);
    }

    // TODO : Separate GameObject and SpriteRenderer methods
    private SpriteRenderer CreateChildSprite(string name, string sprite, float position)
    {
        // Creating the GameObject to hold the SpriteRenderer
        GameObject glow = new GameObject(name);
        glow.transform.parent = this.transform;
        glow.transform.localPosition = new Vector3(0f, 0f, position);
        glow.transform.localEulerAngles = Vector3.zero;
        glow.transform.localScale = Vector3.one;

        // Creating the SpriteRenderer
        SpriteRenderer glowRenderer = glow.AddComponent<SpriteRenderer>();
        glowRenderer.sortingLayerName = "Minion";
        glowRenderer.sprite = Resources.Load<Sprite>(sprite);
        glowRenderer.enabled = false;

        return glowRenderer;
    }

    private void OnMouseEnter()
    {
        whiteGlowRenderer.enabled = true;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.EnableArrowCircle();
        }
    }

    private void OnMouseDown()
    {
        InterfaceManager.Instance.EnableArrow();
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();
    }

    private void OnMouseExit()
    {
        whiteGlowRenderer.enabled = false;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.DisableArrowCircle();
        }
    }
}