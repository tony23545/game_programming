using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopSubdivision : MonoBehaviour
{
	class MeshHelper {
		public Vector3[] vertices;
		public int[] triangles;
	}
		
	private MeshHelper mesh1;
	private MeshHelper mesh2;
	private MeshHelper mesh3;
	private MeshHelper mesh4;

	private int[,,] edgeVertice;
	private int newIndexOfVertices;

	int addEdgeVertice(int v1Index,int v2Index, int v3Index){
		if (v1Index > v2Index) {
			int vTemp = v1Index;
			v1Index = v2Index;
			v2Index = vTemp;
		}

		if (edgeVertice [v1Index,v2Index,0] == 0) {//new vertex
			edgeVertice [v1Index,v2Index,0] = newIndexOfVertices;
			newIndexOfVertices = newIndexOfVertices + 1;
			edgeVertice [v1Index,v2Index,1] = v3Index;
		} else {
			edgeVertice [v1Index,v2Index,2] = v3Index;
		}

		return edgeVertice [v1Index,v2Index,0];
	}


	MeshHelper LoopSubdivide(MeshHelper mesh)
	{
		MeshHelper newMesh = new MeshHelper ();

		int nvertices = mesh.vertices.Length;
		int ntriangles = mesh.triangles.Length / 3;
		print (nvertices);
		print (mesh.triangles.Length);
		print (ntriangles);
		edgeVertice = new int[nvertices,nvertices,3];
		//initialize
		for (int i = 0; i < nvertices; i++) {
			for (int j = 0; j < nvertices; j++) {
				edgeVertice [i,j,0] = 0;
				edgeVertice [i,j,1] = 0;
				edgeVertice [i,j,2] = 0;
			}
		}

		newIndexOfVertices = nvertices;
		int vaIndex, vbIndex, vcIndex;
		int vpIndex, vqIndex, vrIndex;
		int vNindex, vNOpposite1Index, vNOpposite2Index;


		newMesh.triangles = new int[4 * mesh.triangles.Length];

		for (int i = 0; i < ntriangles; i++) {
			vaIndex = mesh.triangles [3 * i];
			vbIndex = mesh.triangles [3 * i + 1];
			vcIndex = mesh.triangles [3 * i + 2];

			vpIndex = addEdgeVertice (vaIndex, vbIndex, vcIndex);
			vqIndex = addEdgeVertice (vbIndex, vcIndex, vaIndex);
			vrIndex = addEdgeVertice (vaIndex, vcIndex, vbIndex);


			int[] newfourTriangle = {vaIndex, vpIndex, vrIndex, vpIndex, vbIndex, vqIndex,
				vrIndex, vqIndex, vcIndex, vrIndex, vpIndex, vqIndex};
			newfourTriangle.CopyTo (newMesh.triangles, 12*i);
		}
		newMesh.vertices = new Vector3[newIndexOfVertices];

		//insert new vertices
		for (int v1 = 0; v1 < nvertices; v1++) {
			for (int v2 = v1+1; v2 < nvertices; v2++) {
				vNindex = edgeVertice [v1,v2,0];
				if (vNindex != 0) {
					vNOpposite1Index = edgeVertice [v1,v2,1];
					vNOpposite2Index = edgeVertice [v1,v2,2];
					if (vNOpposite1Index == 0 || vNOpposite2Index == 0) {
					}
					newMesh.vertices [vNindex] = new Vector3 (
						(float)3.0 * (mesh.vertices [v1].x + mesh.vertices [v2].x) / (float)8.0 + (mesh.vertices [vNOpposite1Index].x + mesh.vertices [vNOpposite2Index].x) / (float)8.0,
						(float)3.0 * (mesh.vertices [v1].y + mesh.vertices [v2].y) / (float)8.0 + (mesh.vertices [vNOpposite1Index].y + mesh.vertices [vNOpposite2Index].y) / (float)8.0,
						(float)3.0 * (mesh.vertices [v1].z + mesh.vertices [v2].z) / (float)8.0 + (mesh.vertices [vNOpposite1Index].z + mesh.vertices [vNOpposite2Index].z) / (float)8.0
					);
				}
			}
		}

		//update existing vertices
		int counter;
		int[] temp = new int[nvertices];
		int[][] oneRingVertices = new int[nvertices][];
		for (int i = 0; i < nvertices; i++) {
			counter = 0;
			for (int j = 0; j < nvertices; j++) {
				if ((i < j && edgeVertice [i,j,0] != 0) || (i > j && edgeVertice [j,i,0] != 0)) {
					temp [counter] = j;
					counter = counter + 1;
				}
			}
			oneRingVertices [i] = new int[counter];
			for (int k = 0; k < counter; k++) {
				oneRingVertices [i][k] = temp [k];
			}
		}
			

		for (int i = 0; i < nvertices; i++) {
			int n = oneRingVertices [i].Length;
			double lambda = (5.0 / 8.0 -Math.Pow((3.0/8.0+Math.Cos(2*Math.PI/n)/4),2.0))/n;
	
			newMesh.vertices [i] = new Vector3((float)(1 - n * lambda) * mesh.vertices [i].x,(float)(1 - n * lambda) * mesh.vertices [i].y,(float)(1 - n * lambda) * mesh.vertices [i].z);
			for (int j = 0; j < n; j++) {
				newMesh.vertices [i] = newMesh.vertices [i] + new Vector3 (
					(float)lambda * mesh.vertices [oneRingVertices [i] [j]].x, 
					(float)lambda * mesh.vertices [oneRingVertices [i] [j]].y, 
					(float)lambda * mesh.vertices [oneRingVertices [i] [j]].z);
			}
		}
		return newMesh;
	}



	void Start()
	{
		Mesh mesh = GetComponent<MeshFilter> ().mesh;

		mesh1 = new MeshHelper ();
		mesh1.vertices = mesh.vertices;
		mesh1.triangles = mesh.triangles;
		mesh2 = LoopSubdivide (mesh1);
		mesh3 = LoopSubdivide (mesh2);
		mesh4 = LoopSubdivide (mesh3);


		mesh.RecalculateNormals ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.Alpha4)) {
			Mesh mesh = GetComponent<MeshFilter> ().mesh;
			mesh.Clear ();

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				mesh.vertices = mesh1.vertices;
				mesh.triangles = mesh1.triangles;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				mesh.vertices = mesh2.vertices;
				mesh.triangles = mesh2.triangles;
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				mesh.vertices = mesh3.vertices;
				mesh.triangles = mesh3.triangles;
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				mesh.vertices = mesh4.vertices;
				mesh.triangles = mesh4.triangles;
			}

			mesh.RecalculateNormals ();
		}
	}
}