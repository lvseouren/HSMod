using UnityEngine;

public class MinionController : MonoBehaviour
{
    private SpriteRenderer greenGlowRenderer;
    private SpriteRenderer whiteGlowRenderer;
    private SpriteRenderer redGlowRenderer;

    private void Start()
    {
        GameObject greenGlow = new GameObject("GreenGlow");
        greenGlow.transform.parent = this.transform;
        greenGlow.transform.localPosition = new Vector3(0f, 0f, 0.01f);
        greenGlow.transform.localEulerAngles = Vector3.zero;
        greenGlow.transform.localScale = Vector3.one;

        greenGlowRenderer = greenGlow.AddComponent<SpriteRenderer>();
        greenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/MinionGreenGlow");
        greenGlowRenderer.enabled = false;

        GameObject whiteGlow = new GameObject("WhiteGlow");
        whiteGlow.transform.parent = this.transform;
        whiteGlow.transform.localPosition = new Vector3(0f, 0f, 0.01f);
        whiteGlow.transform.localEulerAngles = Vector3.zero;
        whiteGlow.transform.localScale = Vector3.one;

        whiteGlowRenderer = whiteGlow.AddComponent<SpriteRenderer>();
        whiteGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/MinionWhiteGlow");
        whiteGlowRenderer.enabled = false;

        GameObject redGlow = new GameObject("RedGlow");
        redGlow.transform.parent = this.transform;
        redGlow.transform.localPosition = new Vector3(0f, 0f, 0.01f);
        redGlow.transform.localEulerAngles = Vector3.zero;
        redGlow.transform.localScale = Vector3.one;

        redGlowRenderer = redGlow.AddComponent<SpriteRenderer>();
        //redGlowRenderer.sprite = Resources.Load<Sprite>("Resources/Sprites/Glows/MinionRedGlow.png");
        redGlowRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        whiteGlowRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        whiteGlowRenderer.enabled = false;
    }
}