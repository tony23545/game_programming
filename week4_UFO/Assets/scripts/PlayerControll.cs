using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb2d;

	public int count;

	public Text countText;
	public Text winText;

	private Vector2 movement;

	public GameObject bullet;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		winText.text = "";
		setCountText ();
		bullet.gameObject.SetActive (false);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (movement.x != 0 || movement.y != 0) {
				bullet.GetComponent<bulletController> ().dir = movement;
			}
			bullet.transform.position = transform.position;
			bullet.gameObject.SetActive (true);
		}
		setCountText ();
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		movement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.AddForce (movement * speed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			setCountText ();
		}
		if (other.gameObject.CompareTag("platinum"))
		{
			other.gameObject.SetActive(false);
			count = count + 2;
			setCountText ();
		}
	}

	void setCountText(){
		countText.text = "Count: " + count.ToString ();

		if (count >= 20) {
			winText.text="You Win!";
		}
	}
		
}
