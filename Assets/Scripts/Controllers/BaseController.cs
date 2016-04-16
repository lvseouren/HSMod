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
}