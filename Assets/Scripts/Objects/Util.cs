using UnityEngine;

// Static class to hold extension methods
public static class Util
{
    // Method to dispose the sprites and their textures in a SpriteRenderer
    public static void Dispose(this SpriteRenderer renderer)
    {
        if (renderer.sprite != null)
        {
            GameObject.Destroy(renderer.sprite);

            if (renderer.sprite.texture != null)
            {
                GameObject.Destroy(renderer.sprite.texture);
            }
        }
    }
}