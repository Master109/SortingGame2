using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour
{
	public string message;

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
		Camera.main.SendMessage(message);
		GameObject.Find("Touch").transform.position = Vector2.zero;
	}
}
