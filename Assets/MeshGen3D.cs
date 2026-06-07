using UnityEngine;


public class MeshGen3D : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    float[] verticesState;
    int[] triangles;


    public int xSize = 10;
    public int zSize = 10;
    public int ySize = 10;

    public float threshold = 0.5f;
    public float noiseScale = 0.2f;


    // public float freq = 2.0f;
    // public float amp = 1.0f;
    // public float persistence = 0.2f;
    // public int oct = 3;
    // public int seed = 0;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        //UpdateMesh();

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1) * (zSize + 1)];
        verticesState = new float[(xSize + 1) * (ySize + 1) * (zSize + 1)];
        int i = 0;
        for (int x = 0; x <= zSize; x++)
        {
            for (int y = 0; y <= xSize; y++)
            {
                for (int z = 0; z <= ySize; z++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    verticesState[i] = Perlin.Noise.Gen3D(x, y, z, noiseScale);
                    i++;
                }
            }

        }



    }

    void UpdateMesh()
    {
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
