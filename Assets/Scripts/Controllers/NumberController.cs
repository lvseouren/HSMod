using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    private Vector3 MainPosition;
    private int Order;
    private float Size;
    private List<SpriteRenderer> Renderers = new List<SpriteRenderer>();

    public static NumberController Create(string name, GameObject parent, Vector3 position, int order, float size)
    {
        // Creating a new GameObject to hold all the components
        GameObject controllerObject = new GameObject(name);
        controllerObject.transform.ChangeParentAt(parent.transform, position);

        // Adding a NumberController to the GameObject
        NumberController numberController = controllerObject.AddComponent<NumberController>();
        numberController.MainPosition = position;
        numberController.Order = order;
        numberController.Size = size;

        return numberController;
    }

    // TODO : Fix for negative numbers
    public void UpdateNumber(int wholeNumber, string color)
    {
        // Destroying all the SpriteRenderers
        DestroyRenderers();

        // Transforming the number to an array of characters
        char[] numberCharacters = wholeNumber.ToString().ToCharArray();

        // Iterating on the array of characters
        for (int i = 0; i < numberCharacters.Length; i++)
        {
            // Calculating the position of the number
            Vector3 targetPosition = new Vector3(i * Size, 0f, 0f);

            // Parsing the character to integer
            int singleNumber = int.Parse(numberCharacters[i].ToString());

            // Creating a SpriteRendere with the number, color and position specified
            SpriteRenderer numberRenderer = CreateNumberRendererAt(singleNumber, color, targetPosition);

            // Adding it to the list of SpriteRenderers
            Renderers.Add(numberRenderer);
        }

        // Moving the parent transform to center the numbers
        this.transform.localPosition = MainPosition - new Vector3((numberCharacters.Length - 1) * Size / 2f, 0f, 0f);
    }

    public void Remove()
    {
        // Destroying all the SpriteRenderers
        DestroyRenderers();

        // Destroying the main GameObject
        Destroy(this.gameObject);
    }

    public void DestroyRenderers()
    {
        // Iterating on the list of SpriteRenderers
        foreach (SpriteRenderer numberRenderer in Renderers)
        {
            // Destroying the SpriteRenderer main GameObject
            Destroy(numberRenderer.gameObject);
        }

        // Clearing the list of SpriteRenderers
        Renderers.Clear();
    }

    public void SetEnabled(bool status)
    {
        // Iterating on the list of SpriteRenderers
        foreach (SpriteRenderer numberRenderer in Renderers)
        {
            // Changing the status of the SpriteRenderer
            numberRenderer.enabled = status;
        }
    }

    public void SetRenderingOrder(int order)
    {
        // Setting the global rendering order
        Order = order;

        // Iterating on the list of SpriteRenderers
        foreach (SpriteRenderer numberRenderer in Renderers)
        {
            // Changing the rendering order of the SpriteRenderer
            numberRenderer.sortingOrder = order;
        }
    }

    private SpriteRenderer CreateNumberRendererAt(int number, string color, Vector3 position)
    {
        // Creating a new GameObject to hold all the components
        GameObject baseObject = new GameObject("NumberRenderer_" + number);
        baseObject.transform.ChangeParentAt(this.transform, position);
        baseObject.transform.localScale = Vector3.one * Size;

        // Adding a SpriteRenderer to the GameObject
        SpriteRenderer spriteRenderer = baseObject.AddComponent<SpriteRenderer>();
        spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = Order;
        spriteRenderer.enabled = true;

        // Loading the sprite into the SpriteRenderer
        spriteRenderer.sprite = SpriteManager.Instance.Numbers[color][number];

        return spriteRenderer;
    }
}