using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolve_sun : MonoBehaviour {

	public float speed = 25.0f;

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, speed * Time.deltaTime, 0);
	}
}
