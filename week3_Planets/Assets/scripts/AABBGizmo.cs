using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AABBGizmo : MonoBehaviour {

	private Stack<Vector3> bounds_max = new Stack<Vector3>();
	private Stack<Vector3> bounds_min = new Stack<Vector3>();

	private Vector3 draw_max, draw_min,cal_r;
	// Use this for initialization
	void Start () {
	}

	void cal_bound(Transform gameT){
		if(gameT.childCount>0){
			if (gameT.childCount == 1) {
				cal_bound (gameT.GetChild (0));
			} else {
				Vector3 max = Vector3.zero;
				Vector3 min = Vector3.zero;
				for (int i = 0; i < gameT.childCount; i++) {
					cal_bound (gameT.GetChild (i));
					if (i == 0) {
						max = bounds_max.Peek ();
						min = bounds_min.Peek ();
					} else {
						max = new Vector3 (Mathf.Max (max.x, bounds_max.Peek ().x), Mathf.Max (max.y, bounds_max.Peek ().y), Mathf.Max (max.z, bounds_max.Peek ().z));
						min = new Vector3 (Mathf.Min (min.x, bounds_min.Peek ().x), Mathf.Min (min.y, bounds_min.Peek ().y), Mathf.Min (min.z, bounds_min.Peek ().z));
					}
				}
				bounds_max.Push (max);
				bounds_min.Push (min);
			}

		} else {
			bounds_max.Push(gameT.GetComponent<Renderer> ().bounds.max);
			bounds_min.Push(gameT.GetComponent<Renderer> ().bounds.min);
		}
	}


	void OnDrawGizmos() {
		cal_bound (this.transform);
		while (bounds_max.Count>0) {
			draw_max = bounds_max.Pop ();
			draw_min = bounds_min.Pop ();
			cal_r = (draw_max - draw_min) / 2;
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube ((draw_max+draw_min)/2,(draw_max-draw_min));
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere ((draw_max + draw_min) / 2, cal_r.magnitude);
		}
	}
	// Update is called once per frame
	void Update () {

	}
}
