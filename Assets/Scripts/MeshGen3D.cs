using UnityEngine;
using static System.Array;
using System;
using System.Text;
using System.Collections.Generic;

// Axes are:
//
//      y
//      |     z
//      |   /
//      | /
//      +----- x
//
// Vertex and edge layout:
//
//            6             7
//            +-------------+               +-----6-------+
//          / |           / |             / |            /|
//        /   |         /   |          11   7         10  5
//    2 +-----+-------+  3  |         +-----+2------+     |
//      |   4 +-------+-----+ 5       |     +-----4-+-----+
//      |   /         |   /           3   8         1   9
//      | /           | /             | /           | /
//    0 +-------------+ 1             +------0------+
//
//

public class MeshGen3D : MonoBehaviour
{
    Mesh mesh;

    private Vector3[] vertices;
    private float[] verticesState;

    private int[] triangles;
    private int[] cubePoints;

    // public static List<int> edgesToConnect = new List<int>();

    private string activeCorners;
    private int indexCorners;
    public List<int> trianglesList = new List<int>();
    public int xSize = 10;
    public int zSize = 10;
    public int ySize = 10;

    public float threshold = 0.5f;
    public float noiseScale = 0.2f;




    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();

    }

    void CreateShape()
    {

        vertices = new Vector3[(xSize + 1) * (ySize + 1) * (zSize + 1)];
        verticesState = new float[vertices.Length];
        int i = 0;
        for (int x = 0; x <= xSize; x++)
        {
            for (int y = 0; y <= ySize; y++)
            {
                for (int z = 0; z <= zSize; z++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    verticesState[i] = Perlin.Noise.Gen3D(x, y, z, noiseScale);
                    i++;
                }
            }
        }

        cubePoints = new int[8];
        activeCorners = string.Empty;
        for (int x = 0; x <= xSize-1; x++)
        {
            for (int y = 0; y <= ySize-1; y++)
            {
                for (int z = 0; z <= zSize-1; z++)
                {
                    cubePoints[0] = IndexOf(vertices, new Vector3(x, y, z));
                    cubePoints[1] = IndexOf(vertices, new Vector3(x + 1, y, z));
                    cubePoints[2] = IndexOf(vertices, new Vector3(x, y + 1, z));
                    cubePoints[3] = IndexOf(vertices, new Vector3(x + 1, y + 1, z));
                    cubePoints[4] = IndexOf(vertices, new Vector3(x, y, z + 1));
                    cubePoints[5] = IndexOf(vertices, new Vector3(x + 1, y, z + 1));
                    cubePoints[6] = IndexOf(vertices, new Vector3(x, y + 1, z + 1));
                    cubePoints[7] = IndexOf(vertices, new Vector3(x + 1, y + 1, z + 1));

                     for (int j = 0; j < cubePoints.Length; j++)
                     {
                        if (verticesState[cubePoints[j]] > threshold)
                        {
                            activeCorners += "1";
                        }
                        else
                        {
                            activeCorners += "0";
                        }
                    }

                    indexCorners = Convert.ToInt32(activeCorners, 2);
                    activeCorners = string.Empty;  // reset activeCorners

                    foreach (int coordinate in TLT.CL.LT[indexCorners]){
                        if (coordinate != -1){
                            trianglesList.Add(coordinate);
                        }

                    }


                 }
             }
        }


    }


    void UpdateMesh()
    {
        triangles = new int[trianglesList.Count];
        triangles = trianglesList.ToArray();
        foreach (int triangle in triangles){
            print(triangle);
        }
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


    private void OnDrawGizmos()
    {

        if (vertices == null)
        {
            return;

        }
        for (int i = 0; i < verticesState.Length; i++)
        {
            if (verticesState[i] >= threshold)
            {
                Gizmos.color = new Color(verticesState[i], 0f, 0f, 1f);
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }
}
