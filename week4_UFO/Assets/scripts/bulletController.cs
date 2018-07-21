using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {

	public GameObject player;
	public Vector2 dir;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = 50.0F;
		dir = new Vector2 (0, 1);
	}

	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown("space")){
			print ("dffd");
			transform.position = player.gameObject.transform.position;
			dir = player.GetComponent<PlayerControll>().movement;
			print (dir);
			gameObject.SetActive (true);
		}
		if (gameObject.activeSelf) {
			transform.position = transform.position + new Vector3(dir.x,dir.y,0) * Time.deltaTime*speed;
		}*/

		transform.position = transform.position + new Vector3(dir.x,dir.y,0) * Time.deltaTime*speed;


	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("PickUp"))
		{
			gameObject.SetActive(false);
			other.gameObject.SetActive (false);
			player.GetComponent<PlayerControll>().count = player.GetComponent<PlayerControll>().count + 1;
		}
		if (other.gameObject.CompareTag("platinum"))
		{
			gameObject.SetActive(false);
			other.gameObject.SetActive (false);
			player.GetComponent<PlayerControll>().count = player.GetComponent<PlayerControll>().count + 2;
		}
		if (other.gameObject.CompareTag ("background")) {
			gameObject.SetActive(false);
		}
	}

}
