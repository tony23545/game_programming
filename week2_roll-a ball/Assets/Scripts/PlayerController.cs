using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public Text countText;
	public Text winText;
	public Text clockText;
	public float speed=5;

	private int count;

	private float timeSpend = 0.0f;
	int hour,min,sec;
	private bool start;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		start = false;
		SetCountText ();
		SetClock ();
		winText.text = "";
	}

	void FixedUpdate(){
		timeSpend += Time.deltaTime;
		SetClock ();
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		if (!start) {
			timeSpend = 0;
			if (moveVertical != 0 || moveHorizontal != 0)
				start = true;
		}
		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);
		rb.AddForce(movement*speed);
		//press space causes the ball to jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			//jump only if it is on the ground
			if (transform.position.y == 0.5) {
				rb.AddForce (Vector3.up * 125);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")){
			count++;
			other.gameObject.SetActive (false);
			SetCountText ();
		}
	}

	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text="You Win!";
		}
	}

	void SetClock(){
		if (count < 12 && start) {
			hour = (int)timeSpend / 3600;
			min = ((int)timeSpend - hour * 3600) / 60;
			sec = (int)timeSpend - hour * 3600 - min * 60;
		}
		clockText.text = string.Format ("{0:D2}:{1:D2}:{2:D2}",hour,min,sec);
	}

}