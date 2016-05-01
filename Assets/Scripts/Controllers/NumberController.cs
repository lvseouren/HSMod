using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    private Vector3 MainPosition;
    private int Order;
    private List<SpriteRenderer> Renderers = new List<SpriteRenderer>();

    public static NumberController Create(string name, GameObject parent, Vector3 position, int order)
    {
        GameObject controllerObject = new GameObject(name);
        controllerObject.transform.parent = parent.transform;
        controllerObject.transform.localPosition = position;
        controllerObject.transform.localEulerAngles = Vector3.zero;
        controllerObject.transform.localScale = Vector3.one;

        NumberController numberController = controllerObject.AddComponent<NumberController>();
        numberController.MainPosition = position;
        numberController.Order = order;

        return numberController;
    }

    public void UpdateNumber(int wholeNumber, string color)
    {
        DestroyRenderers();

        string numberText = wholeNumber.ToString();
        char[] numberCharacters = numberText.ToCharArray();

        for (int i = 0; i < numberCharacters.Length; i++)
        {
            int number = int.Parse(numberCharacters[i].ToString());

            SpriteRenderer numberRenderer = CreateNumberRenderer(number, color);
            Renderers.Add(numberRenderer);

            numberRenderer.transform.localPosition = new Vector3(i * 0.6f, 0f, 0f);
        }

        this.transform.localPosition = MainPosition - new Vector3((numberCharacters.Length - 1) * 0.3f, 0f, 0f);
    }

    public void DestroyRenderers()
    {
        foreach (SpriteRenderer renderer in Renderers)
        {
            renderer.DisposeSprite();
            Destroy(renderer);
        }

        Renderers.Clear();
    }

    public void SetEnabled(bool status)
    {
        foreach (SpriteRenderer renderer in Renderers)
        {
            renderer.enabled = status;
        }
    }

    private SpriteRenderer CreateNumberRenderer(int number, string color)
    {
        GameObject baseObject = new GameObject("NumberRenderer_" + number);
        baseObject.transform.parent = this.transform;
        baseObject.transform.localPosition = Vector3.zero;
        baseObject.transform.localEulerAngles = Vector3.zero;
        baseObject.transform.localScale = Vector3.one * 0.5f;

        SpriteRenderer spriteRenderer = baseObject.AddComponent<SpriteRenderer>();
        spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        spriteRenderer.sprite = SpriteManager.Instance.Numbers[color][number];
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = Order;
        spriteRenderer.enabled = false;

        return spriteRenderer;
    }
}