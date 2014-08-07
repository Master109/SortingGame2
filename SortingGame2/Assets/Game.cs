using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public float sphereCreateRate = 3;
	float sphereCreateTime;
	public GameObject sphereG;
	public GameObject sphereR;
	public float sphereCreateRateMultiplier = .975f;
	GameObject go;
	public int score;
	public bool onDevice;
	bool pTouching;

	// Use this for initialization
	void Start ()
	{
		sphereCreateTime = sphereCreateRate;
		int spaceIDToCreateOn = Mathf.RoundToInt(Random.Range(0, GameObject.FindGameObjectsWithTag("Space").Length));
		GameObject spaceToCreateOn = (GameObject) GameObject.FindGameObjectsWithTag("Space")[spaceIDToCreateOn];
		int r = Mathf.RoundToInt(Random.Range(0, 2));
		if (r == 0)
			go = (GameObject) GameObject.Instantiate(sphereG, spaceToCreateOn.transform.position, Quaternion.identity);
		else
			go = (GameObject) GameObject.Instantiate(sphereR, spaceToCreateOn.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 1 && !pTouching)
		{
			GameObject.Find("Touch").transform.position = Input.touches[0].position;
		}
		if (Input.touchCount > 1)
			pTouching = true;
		else
			pTouching = false;
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Moveable"))
				g.transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal"));
		else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			foreach (GameObject g4 in GameObject.FindGameObjectsWithTag("Moveable"))
			{
				g4.name = "Sphere";
				//if (g4.transform.position.y + Input.GetAxisRaw("Vertical") > -2 && g4.transform.position.y + Input.GetAxisRaw("Vertical") < 2)
				//{
				g4.transform.Translate(Vector2.up * Input.GetAxisRaw("Vertical"));
				if (g4.transform.position.y == -(GameObject.Find("Board").transform.localScale.y / 2 + .5f))
					g4.transform.Translate(Vector2.up * GameObject.Find("Board").transform.localScale.y);
				else if (g4.transform.position.y == GameObject.Find("Board").transform.localScale.y / 2 + .5f)
					g4.transform.Translate(-Vector2.up * GameObject.Find("Board").transform.localScale.y);
				g4.name = string.Concat(g4.name, "(Moved)");
				//}
			}
			foreach (GameObject g3 in GameObject.FindGameObjectsWithTag("Moveable"))
				foreach (GameObject g2 in GameObject.FindGameObjectsWithTag("Moveable"))
					if (g2.transform.position == g3.transform.position && g3.name.Contains("Moved") && g2 != g3)
				{
					Debug.Log (g3.transform.position);
					g3.transform.Translate(-Vector2.up * Input.GetAxisRaw("Vertical"));
					break;
				}
		}
		if (Time.timeSinceLevelLoad > sphereCreateTime)
		{
			int i = 0;
			while (true)
			{
				if (i > GameObject.FindGameObjectsWithTag("Space").Length * GameObject.FindGameObjectsWithTag("Space").Length * 2)
				{
					Application.LoadLevel(Application.loadedLevel);
					return;
				}
				int spaceIDToCreateOn = Mathf.RoundToInt(Random.Range(0, GameObject.FindGameObjectsWithTag("Space").Length));
				GameObject spaceToCreateOn = (GameObject) GameObject.FindGameObjectsWithTag("Space")[spaceIDToCreateOn];
				bool willCollide = false;
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("Moveable"))
					if (g.transform.position == spaceToCreateOn.transform.position)
						willCollide = true;
				if (!willCollide)
				{
					int r = Mathf.RoundToInt(Random.Range(0, 2));
					if (r == 0)
						go = (GameObject) GameObject.Instantiate(sphereG, spaceToCreateOn.transform.position, Quaternion.identity);
					else
						go = (GameObject) GameObject.Instantiate(sphereR, spaceToCreateOn.transform.position, Quaternion.identity);
					break;
				}
				if (GameObject.FindGameObjectsWithTag("Moveable").Length == GameObject.FindGameObjectsWithTag("Space").Length)
					break;
				i ++;
			}
			sphereCreateRate *= sphereCreateRateMultiplier;
			sphereCreateTime += sphereCreateRate;
		}
		//if (GameObject.FindGameObjectsWithTag("Moveable").Length == GameObject.FindGameObjectsWithTag("Space").Length)
		//	Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Left ()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Moveable"))
			g.transform.Translate(-Vector2.right);
	}
	
	public void Right ()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Moveable"))
			g.transform.Translate(-Vector2.right);
	}
	
	public void Down ()
	{
		foreach (GameObject g4 in GameObject.FindGameObjectsWithTag("Moveable"))
		{
			g4.name = "Sphere";
			//if (g4.transform.position.y + Input.GetAxisRaw("Vertical") > -2 && g4.transform.position.y + Input.GetAxisRaw("Vertical") < 2)
			//{
			g4.transform.Translate(-Vector2.up * 1);
			if (g4.transform.position.y == -(GameObject.Find("Board").transform.localScale.y / 2 + .5f))
				g4.transform.Translate(Vector2.up * GameObject.Find("Board").transform.localScale.y);
			g4.name = string.Concat(g4.name, "(Moved)");
			//}
		}
		foreach (GameObject g3 in GameObject.FindGameObjectsWithTag("Moveable"))
			foreach (GameObject g2 in GameObject.FindGameObjectsWithTag("Moveable"))
				if (g2.transform.position == g3.transform.position && g3.name.Contains("Moved") && g2 != g3)
				{
					Debug.Log (g3.transform.position);
					g3.transform.Translate(-Vector2.up * -1);
					break;
				}
	}
	
	public void Up ()
	{
		foreach (GameObject g4 in GameObject.FindGameObjectsWithTag("Moveable"))
		{
			g4.name = "Sphere";
			//if (g4.transform.position.y + Input.GetAxisRaw("Vertical") > -2 && g4.transform.position.y + Input.GetAxisRaw("Vertical") < 2)
			//{
			g4.transform.Translate(Vector2.up * 1);
			if (g4.transform.position.y == GameObject.Find("Board").transform.localScale.y / 2 + .5f)
				g4.transform.Translate(-Vector2.up * GameObject.Find("Board").transform.localScale.y);
			g4.name = string.Concat(g4.name, "(Moved)");
			//}
		}
		foreach (GameObject g3 in GameObject.FindGameObjectsWithTag("Moveable"))
			foreach (GameObject g2 in GameObject.FindGameObjectsWithTag("Moveable"))
				if (g2.transform.position == g3.transform.position && g3.name.Contains("Moved") && g2 != g3)
				{
					Debug.Log (g3.transform.position);
					g3.transform.Translate(Vector2.up * -1);
					break;
				}
	}
	
	void OnGUI ()
	{
		GUI.Label(new Rect(0, 0, 9999999999, 50), "Best score: " + PlayerPrefs.GetInt("Score", 0));
		GUI.Label(new Rect(0, 10, 9999999999, 50), "Score: " + score);
	}
}
