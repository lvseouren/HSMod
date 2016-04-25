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

    public virtual void UpdateText() { }

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

        TextMesh textMesh = meshObject.AddComponent<TextMesh>();
        textMesh.font = Resources.Load<Font>("Fonts/Belwe-Bold");
        textMesh.fontSize = 20;

        MeshRenderer meshRenderer = meshObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = textMesh.font.material;
        meshRenderer.sortingLayerName = "Game";
        meshRenderer.sortingOrder = order;

        GameObject cloneMeshObject = Instantiate(meshObject);
        cloneMeshObject.transform.parent = meshObject.transform;
        cloneMeshObject.transform.localPosition = Vector3.zero;
        cloneMeshObject.transform.localEulerAngles = Vector3.zero;
        cloneMeshObject.transform.localScale = Vector3.one;
        cloneMeshObject.GetComponent<TextMesh>().color = Color.black;
        cloneMeshObject.GetComponent<TextMesh>().text = "0";
        cloneMeshObject.GetComponentInChildren<MeshRenderer>().sortingOrder = order - 1;

        return textMesh;

        // Creating a clone for the outline
        GameObject childMeshObject = new GameObject(name + "_Child");
        childMeshObject.transform.parent = meshObject.transform;
        childMeshObject.transform.localScale = Vector3.one;
        childMeshObject.transform.localEulerAngles = Vector3.zero;
        childMeshObject.transform.localPosition = Vector3.zero;

        TextMesh childTextMesh = childMeshObject.AddComponent<TextMesh>();
        childTextMesh.font = Resources.Load<Font>("Fonts/Belwe-Bold");
        childTextMesh.fontSize = 20;
        childTextMesh.color = Color.black;
        childTextMesh.text = "0";

        MeshRenderer childMeshRenderer = childTextMesh.GetComponentInChildren<MeshRenderer>();
        childMeshRenderer.material = childTextMesh.font.material;
        childMeshRenderer.sortingLayerName = "Game";
        childMeshRenderer.sortingOrder = order - 1;
        
        return textMesh;
    }

    public void SetGreenRenderer(bool status)
    {
        if (this.GreenGlowRenderer != null)
        {
            this.GreenGlowRenderer.enabled = status;
        }
    }

    public void SetWhiteRenderer(bool status)
    {
        if (this.WhiteGlowRenderer != null)
        {
            this.WhiteGlowRenderer.enabled = status;
        }
    }

    public void SetBlueRenderer(bool status)
    {
        if (this.BlueGlowRenderer != null)
        {
            this.BlueGlowRenderer.enabled = status;
        }
    }

    public void SetRedRenderer(bool status)
    {
        if (this.RedGlowRenderer != null)
        {
            this.RedGlowRenderer.enabled = status;
        }
    }
}