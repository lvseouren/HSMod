using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public SpriteRenderer GreenGlowRenderer;
    public SpriteRenderer WhiteGlowRenderer;
    public SpriteRenderer RedGlowRenderer;

    public virtual void Initialize() { }

    public virtual void Remove() { }

    public virtual void UpdateSprites() { }

    public virtual void UpdateNumbers() { }

    protected SpriteRenderer CreateRenderer(string name, Vector3 scale, Vector3 position, int order)
    {
        // Creating a GameObject to hold the SpriteRenderer
        GameObject rendererObject = new GameObject(name);
        rendererObject.transform.parent = this.transform;
        rendererObject.transform.localEulerAngles = Vector3.zero;
        rendererObject.transform.localPosition = position;
        rendererObject.transform.localScale = scale;

        // Creating the SpriteRenderer and adding it to the GameObject
        SpriteRenderer spriteRenderer = rendererObject.AddComponent<SpriteRenderer>();
        spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = order;
        spriteRenderer.enabled = false;

        return spriteRenderer;
    }

    protected TextMesh CreateText(string name, Vector3 position, int order)
    {
        // Creating a GameObject to hold the TextMesh
        GameObject meshObject = new GameObject(name);
        meshObject.transform.parent = this.transform;
        meshObject.transform.localEulerAngles = Vector3.zero;
        meshObject.transform.localPosition = position;
        meshObject.transform.localScale = Vector3.one * 0.5f;

        // Creating a TextMesh in the new object
        TextMesh textMesh = meshObject.AddComponent<TextMesh>();
        textMesh.font = Resources.Load<Font>("Fonts/Belwe-Bold");
        textMesh.fontSize = 20;
        textMesh.alignment = TextAlignment.Center;
        textMesh.anchor = TextAnchor.MiddleCenter;

        // Modifying the renderer of the TextMesh to match the materials
        MeshRenderer meshRenderer = meshObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("Materials/TextOutline");
        meshRenderer.material.SetTexture("_FontTexture", textMesh.font.material.mainTexture);
        meshRenderer.sortingLayerName = "Default";
        meshRenderer.sortingOrder = order;

        return textMesh;
    }

    public void SetGreenRenderer(bool status)
    {
        if (GreenGlowRenderer != null)
        {
            GreenGlowRenderer.enabled = status;
        }
    }

    public void SetWhiteRenderer(bool status)
    {
        if (WhiteGlowRenderer != null)
        {
            WhiteGlowRenderer.enabled = status;
        }
    }

    public void SetRedRenderer(bool status)
    {
        if (RedGlowRenderer != null)
        {
            RedGlowRenderer.enabled = status;
        }
    }
}