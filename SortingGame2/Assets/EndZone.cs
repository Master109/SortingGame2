using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour
{
	public Material green;
	public Material red;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.renderer.sharedMaterial == renderer.sharedMaterial)
		{
			Destroy(other.gameObject);
			Camera.main.GetComponent<Game>().score ++;
			if (Camera.main.GetComponent<Game>().score > PlayerPrefs.GetInt("Score", 0))
				PlayerPrefs.SetInt("Score", Camera.main.GetComponent<Game>().score);
			if (renderer.sharedMaterial == red)
				renderer.sharedMaterial = green;
			else
				renderer.sharedMaterial = red;
		}
		else
			Application.LoadLevel(Application.loadedLevel);
	}
}
