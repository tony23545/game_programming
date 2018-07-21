using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class nav : MonoBehaviour {

	// Use this for initialization

	private NavMeshAgent agent;
	public Transform target;
	public Vector3 temPos;

	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.name == "Terrain") {
					temPos = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
				}
			}
			agent.SetDestination (temPos);
		}
	}
		
	
}
