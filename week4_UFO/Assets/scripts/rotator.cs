using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {
	public GameObject player;
	private Vector3 toPlayer;
	private float speed = 0.05F;

	void Update(){
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
		toPlayer = transform.position - player.transform.position;
		if (toPlayer.magnitude < 6) {
			transform.position = transform.position - toPlayer*Time.deltaTime*speed;
		}

	}
}
