using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    private List<SpriteRenderer> Renderers = new List<SpriteRenderer>();

    public static NumberController Create(string name, GameObject parent, Vector3 position)
    {
        GameObject controllerObject = new GameObject(name);
        controllerObject.transform.parent = parent.transform;
        controllerObject.transform.localPosition = position;

        return controllerObject.AddComponent<NumberController>();
    }

    public void UpdateNumber(int wholeNumber, string color)
    {
        Destroy();

        string numberText = wholeNumber.ToString();
        char[] numberCharacters = numberText.ToCharArray();

        for (int i = 0; i < numberCharacters.Length; i++)
        {
            int number = int.Parse(numberCharacters[i].ToString());

            CreateNumberRenderer(number, color);
        }
    }

    public void Destroy()
    {
        foreach (SpriteRenderer renderer in Renderers)
        {
            renderer.DisposeSprite();
            Destroy(renderer);
        }

        Renderers.Clear();
    }

    public SpriteRenderer CreateNumberRenderer(int number, string color)
    {
        GameObject baseObject = new GameObject("NumberRenderer_" + number);
        baseObject.transform.parent = this.transform;
        baseObject.transform.localPosition = Vector3.zero;

        SpriteRenderer renderer = baseObject.AddComponent<SpriteRenderer>();

        Sprite[] numberSprites = Resources.LoadAll<Sprite>("Sprites/General/Numbers" + color);

        renderer.sprite = numberSprites[number];
    }
}