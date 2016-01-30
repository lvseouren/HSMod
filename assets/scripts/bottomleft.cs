using UnityEngine;

public class BottomLeft : MonoBehaviour {
	public int counter;
	public Texture2D left_bottom2 ;
	void Start ()
	{

	}

	void Update ()
	{
		
//		currentTextures&= textures.Length;
//		GetComponent<Renderer>().material.mainTexture = textures[currentTextures];
		if (counter == 10) {
			counter = 0;
			textureswaping();
		}

	}

	void textureswaping()
	{
	GetComponent<Renderer>().material.mainTexture = left_bottom2;
	}

	void OnMouseDown ()
	{
		counter += 1;
	}

}