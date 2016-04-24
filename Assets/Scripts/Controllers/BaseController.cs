using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public SpriteRenderer GreenGlowRenderer;
    public SpriteRenderer WhiteGlowRenderer;
    public SpriteRenderer BlueGlowRenderer;
    public SpriteRenderer RedGlowRenderer;

    public virtual void Initialize() { }

    public virtual void Remove() { }

    public virtual void UpdateSprites() { }

    protected SpriteRenderer CreateRenderer(string name, Vector3 scale, Vector3 position, int order)
    {
        // Creating a GameObject to hold the SpriteRenderer
        GameObject rendererObject = new GameObject(name);
        rendererObject.transform.parent = this.transform;
        rendererObject.transform.localEulerAngles = Vector3.zero;
        rendererObject.transform.localPosition = position;
        rendererObject.transform.localScale = scale;

        // Creating the SpriteRenderer and adding it to the GameObject
        SpriteRenderer renderer = rendererObject.AddComponent<SpriteRenderer>();
        renderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        renderer.sortingLayerName = "Game";
        renderer.sortingOrder = order;
        renderer.enabled = false;

        return renderer;
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

    public void SetBlueRenderer(bool status)
    {
        if (BlueGlowRenderer != null)
        {
            BlueGlowRenderer.enabled = status;
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