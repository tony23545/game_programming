using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
	private MeshFilter filter;
	private Mesh mesh;

	int mesh_size = 4;

	private Vector3[] vertices;
	private Vector3[] vertices2;


	private int[] triangles;

	private int space_press = 0;

	private int[][] rise_ver = new int[7][];


	// Use this for initialization
	void Start () {

		mesh = GetComponent<MeshFilter>().mesh;


		generate_vertice ();
		generate_tri ();
		rise_ver [0] = new int[4] {0,8,72,80};
		rise_ver [1] = new int[1]{40};
		rise_ver [2] = new int[4]{ 4, 36, 44, 76 };
		rise_ver [3] = new int[4]{ 20, 24, 56, 60 };
		rise_ver [4] = new int[12]{ 2, 6, 18, 22, 26, 38, 42, 54, 58, 62, 74, 78 };
		rise_ver [5] = new int[16]{ 10, 12, 14, 16, 28, 30, 32, 34, 46, 48, 50, 52, 64, 66, 68, 70 };
		rise_ver [6] = new int[40] ;
		for (int i = 0; i<40;i++) {
			rise_ver [6][i] = 2 * i + 1;
		}

		//generate ();
	}

	void generate_vertice(){
		int count = 0;
		vertices = new Vector3[(mesh_size * 2 + 1) * (mesh_size * 2 + 1)];

		for(int x = -mesh_size;x<=mesh_size;x++){
			for(int y = -mesh_size; y<=mesh_size;y++){
				vertices [count] = new Vector3 (x,y,0);
				//Debug.Log (count + "gg" + vertices[count]);
				//uvs [count] = new Vector2 (1f*(x+mesh_size)/(2f*mesh_size),1f*(z+mesh_size)/(2f*mesh_size));
				//vertices [count] = new Vector3 (uvs[count][0]-0.5f,0f,uvs[count][1]-0.5f);
				count++;
			}
		}
		//print (count);
		mesh.vertices = vertices;
		//mesh.uv = uvs;
	}

	void generate_tri(){
		//two triangles, six vertices for each square
		triangles = new int[(mesh_size*2) * (mesh_size*2) * 6];

		int triPoint = 0;
		int triFocus = 0;

		for (int t = 0;t < triangles.Length; t++) {
			switch (triPoint) {
			case 0:
				triangles [t] = triFocus;
				triPoint++;

				break;
			case 1:
				triangles [t] = triFocus + 2 * mesh_size + 1;
				triPoint++;

				break;
			case 2:
				if (triFocus % 2 == 0) {
					triangles [t] = triFocus + 2 * mesh_size + 2;

				} else {
					triangles [t] = triFocus + 1;

				}
				triPoint++;

				break;
			case 3:
				triangles [t] = triFocus + 2 * mesh_size + 2;

				triPoint++;

				break;
			case 4:
				triangles [t] = triFocus + 1;

				triPoint++;

				break;
			case 5:
				if (triFocus % 2 == 0) {
					triangles [t] = triFocus;

				} else {
					triangles [t] = triFocus + 2 * mesh_size + 1;

				}
				triPoint = 0;
				if ((triFocus+2) % (mesh_size * 2 + 1 ) == 0)
					triFocus += 2;
				else
					triFocus++;
				
				break;
			default:
				triPoint = 0;
				break;		
			}
		}
		mesh.triangles = triangles;

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (space_press < 7) {
				vertices = mesh.vertices;

				for (int i = 0; i < rise_ver [space_press].Length; i++) {
					vertices [rise_ver [space_press] [i]].z = 1;
				}
				mesh.vertices = vertices;
				space_press++;
			} else {
				
				vertices2 = vertices;
				//the first four cornors
				for (int t = 0; t < rise_ver [0].Length; t++) {
					vertices2 [rise_ver [0] [t]].z = Random.Range(-1f,1f);
				}
				//diamond step

				vertices2[rise_ver[1][0]].z = (vertices2[0].z+vertices2[8].z+vertices2[72].z+vertices2[80].z)/4f + Random.Range(-1f,1f)/2f;
				//square step
				for (int t = 0; t < rise_ver [2].Length; t++) {
					int id = rise_ver [2] [t];
					if (t % 3f == 0) {
						vertices2 [id].z = (vertices2 [id - 4].z + vertices2 [id + 4].z) / 2f + Random.Range(-1f,1f)/2f;
					} else {
						vertices2 [id].z = (vertices2 [id - 36].z + vertices2 [id + 36].z) / 2f + Random.Range(-1f,1f)/2f;
					}
				}

				//diomond step
				for (int t = 0; t < rise_ver [3].Length; t++) {
					int id = rise_ver [3] [t];
					vertices2 [id].z = (vertices2 [id - 20].z + vertices2 [id - 16].z + vertices2 [id + 16].z + vertices2 [id + 20].z) / 4f + Random.Range(-1f,1f) / 4f;
				}
				//square step
				for (int t = 0; t < rise_ver [4].Length; t++) {
					int id = rise_ver [4] [t];
					if (t % 5f < 2f) {
						vertices2 [id].z = (vertices2 [id - 2].z + vertices2 [id + 2].z) / 2f + Random.Range(-1f,1f) / 4f;
					} else {
						vertices2 [id].z = (vertices2 [id - 18].z + vertices2 [id + 18].z) / 2f + Random.Range(-1f,1f) / 4f;
					}
				}

				//diamond step
				for (int t = 0; t < rise_ver [5].Length; t++) {
					int id = rise_ver [5] [t];
					vertices2 [id].z = (vertices2 [id - 10].z + vertices2 [id - 8].z + vertices2 [id + 8].z + vertices2 [id + 10].z) / 4f + Random.Range(-1f,1f) / 8f;
				}
				//square step
				for (int t = 0; t < rise_ver [6].Length; t++) {
					int id = rise_ver [6] [t];
					if (t % 9f < 4) {
						vertices2 [id].z = (vertices2 [id - 1].z + vertices2 [id + 1].z) / 2f + Random.Range(-1f,1f) / 8f;
					} else {
						vertices2 [id].z = (vertices2 [id - 9].z + vertices2 [id + 9].z) / 2f + Random.Range(-1f,1f) / 8f;
					}
				}

				mesh.vertices = vertices2;

				Color[] colors = new Color[vertices2.Length];

				for (int t = 0; t < vertices2.Length; t++) {
					if (vertices2 [t].z < 0.4f) {
						colors.SetValue (new Color (0, 0.8f + 0.2f * vertices2 [t].z/0.4f, 0), t);
					} else if (vertices2 [t].z < 0.6f) {
						colors.SetValue (new Color (0.56f * (vertices2 [t].z - 0.4f)/0.2f, 0.27f + (0.6f - vertices2 [t].z)/0.2f, 0.16f * (vertices2 [t].z - 0.4f)/0.2f), t);
					} else {
						colors.SetValue (new Color (0.56f + 0.44f*(vertices2 [t].z - 0.6f), 0.27f + 0.73f*(vertices2 [t].z - 0.6f), 0.16f + 0.84f*(vertices2 [t].z - 0.6f)), t);
					}
				}

				mesh.colors = colors;
			}
		}
	}
}
