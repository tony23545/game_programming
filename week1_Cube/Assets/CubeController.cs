using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public float spinSpeed = 20f;
	//public float moveSpeed = .00001f;
		// Use this for initialization
	void Start () {
		//Extension2: the cube is in red rather the default when the game start
		GetComponent<Renderer> ().material.color = Color.red;
	}
	
	// Update is called once per frame

	void Update()
	{
		//Extension1: cube spin continuously
		transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

		//press the arrow buttons and move the cube in XZ
		if (Input.GetKey (KeyCode.UpArrow))
			transform.Translate (Vector3.forward * Time.deltaTime);
		if (Input.GetKey (KeyCode.DownArrow))
			transform.Translate (Vector3.back * Time.deltaTime);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.Translate (Vector3.right * Time.deltaTime);
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (Vector3.left * Time.deltaTime);


		if (Input.GetKeyDown(KeyCode.R))
			GetComponent<Renderer> ().material.color = Color.red;
		if (Input.GetKeyDown(KeyCode.G))
			GetComponent<Renderer>().material.color = Color.green;
		if (Input.GetKeyDown(KeyCode.B))
			GetComponent<Renderer>().material.color = Color.blue;


		//Extension3: hold Q will scale the cube up while W will scale the cube down
		if (Input.GetKey (KeyCode.Q))
			transform.localScale += new Vector3 (1f, 1f, 1f) * Time.deltaTime;
		if (Input.GetKey (KeyCode.W))
			transform.localScale -= new Vector3 (1f, 1f, 1f) * Time.deltaTime;
	}

}
