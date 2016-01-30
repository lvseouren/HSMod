using UnityEngine;

public class textureswap : MonoBehaviour {
	public Texture[] textures;
	public int currentTextures;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			currentTextures&= textures.Length;
			GetComponent<Renderer>().material.mainTexture = textures[currentTextures];
	}
	void OnMouseDown ()
	{
		currentTextures++;
	}
}
