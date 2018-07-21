using UnityEngine;
using System.Collections;

public class HoverMotor : MonoBehaviour {

	public float speed;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;

	void Start(){
		speed = 15f;

	}

	void Awake () 
	{
		carRigidbody = GetComponent <Rigidbody>();
	}

	void Update () 
	{
		powerInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown (KeyCode.Space)) {
			carRigidbody.AddForce (0f, 1000f, 0f);
		}
	}

	void FixedUpdate()
	{
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, hoverHeight))
		{
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

	}
}