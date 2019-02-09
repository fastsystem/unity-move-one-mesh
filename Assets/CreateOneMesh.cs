using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CreateOneMesh : MonoBehaviour
{
    private Mesh mesh = null;

    private int pointMax = 10000; // Cubeの最大数

    private List<Vector3> vertices = new List<Vector3>();

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        InitMesh();
    }

    void OnDistroy()
    {
        mesh.Clear();
    }

    void Update()
    {
        // 更新後のキューブの位置を定義する
        var points = new List<Vector3>();
        for (int x = -50; x < 50; x++)
        {
            for (int z = -50; z < 50; z++)
            {
                var y = Mathf.Sin(Mathf.Sqrt(x * x + z * z) / 4 + Time.realtimeSinceStartup) * 3;
                points.Add(new Vector3(x, y, z));
            }
        }

        UpdateMesh(points);
    }

    private void InitMesh()
    {
        vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>();
        for (int i = 0; i < pointMax; i++)
        {
            int triangleOffset = vertices.Count;
            // 頂点の設定
            // ８点だと法線の計算で影が上手く処理できないので、２４点で設定する
            vertices.AddRange(new Vector3[] {
                new Vector3 (0, 0, 0), // face front
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0), // face back
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0), // face top
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0), // face bottom
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0), // face right
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0), // face left
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
                new Vector3 (0, 0, 0),
            });

            // Normalsの定義
            normals.AddRange(new Vector3[] {
                new Vector3 (0, 0, -1), // face front
                new Vector3 (0, 0, -1),
                new Vector3 (0, 0, -1),
                new Vector3 (0, 0, -1),
                new Vector3 (0, 0, 1), // face back
                new Vector3 (0, 0, 1),
                new Vector3 (0, 0, 1),
                new Vector3 (0, 0, 1),
                new Vector3 (0, 1, 0), // face top
                new Vector3 (0, 1, 0),
                new Vector3 (0, 1, 0),
                new Vector3 (0, 1, 0),
                new Vector3 (0, -1, 0), // face bottom
                new Vector3 (0, -1, 0),
                new Vector3 (0, -1, 0),
                new Vector3 (0, -1, 0),
                new Vector3 (1, 0, 0), // face right
                new Vector3 (1, 0, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (-1, 0, 0), // face left
                new Vector3 (-1, 0, 0),
                new Vector3 (-1, 0, 0),
                new Vector3 (-1, 0, 0),
            });

            // 面の設定
            triangles.AddRange(new int[] {
                triangleOffset + 0, triangleOffset + 2, triangleOffset + 1, //face front
                triangleOffset + 0, triangleOffset + 3, triangleOffset + 2,
                triangleOffset + 5, triangleOffset + 4, triangleOffset + 7, //face back
                triangleOffset + 5, triangleOffset + 7, triangleOffset + 6,
                triangleOffset + 8, triangleOffset + 10, triangleOffset + 9, //face top
                triangleOffset + 8, triangleOffset + 11, triangleOffset + 10,
                triangleOffset + 12, triangleOffset + 14, triangleOffset + 13, //face bottom
                triangleOffset + 12, triangleOffset + 15, triangleOffset + 14,
                triangleOffset + 16, triangleOffset + 18, triangleOffset + 17, //face right
                triangleOffset + 16, triangleOffset + 19, triangleOffset + 18,
                triangleOffset + 20, triangleOffset + 22, triangleOffset + 21, //face left
                triangleOffset + 20, triangleOffset + 23, triangleOffset + 22,
            });
        }

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();
        // mesh.Optimize();
        // mesh.RecalculateNormals();
    }

    public void UpdateMesh(List<Vector3> points)
    {
        var v0 = -0.5f;
        var v1 = 0.5f;

        for (int i = 0; i < points.Count; i++)
        {
            float x = points[i].x;
            float y = points[i].y;
            float z = points[i].z;
            int idx = i * 24;

            vertices[idx + 0] = new Vector3(x + v0, y + v0, z + v0); // face front
            vertices[idx + 1] = new Vector3(x + v1, y + v0, z + v0);
            vertices[idx + 2] = new Vector3(x + v1, y + v1, z + v0);
            vertices[idx + 3] = new Vector3(x + v0, y + v1, z + v0);
            vertices[idx + 4] = new Vector3(x + v0, y + v1, z + v1); // face back
            vertices[idx + 5] = new Vector3(x + v1, y + v1, z + v1);
            vertices[idx + 6] = new Vector3(x + v1, y + v0, z + v1);
            vertices[idx + 7] = new Vector3(x + v0, y + v0, z + v1);
            vertices[idx + 8] = new Vector3(x + v0, y + v1, z + v1); // face top
            vertices[idx + 9] = new Vector3(x + v0, y + v1, z + v0);
            vertices[idx + 10] = new Vector3(x + v1, y + v1, z + v0);
            vertices[idx + 11] = new Vector3(x + v1, y + v1, z + v1);
            vertices[idx + 12] = new Vector3(x + v1, y + v0, z + v1); // face bottom
            vertices[idx + 13] = new Vector3(x + v1, y + v0, z + v0);
            vertices[idx + 14] = new Vector3(x + v0, y + v0, z + v0);
            vertices[idx + 15] = new Vector3(x + v0, y + v0, z + v1);
            vertices[idx + 16] = new Vector3(x + v1, y + v1, z + v1); // face right
            vertices[idx + 17] = new Vector3(x + v1, y + v1, z + v0);
            vertices[idx + 18] = new Vector3(x + v1, y + v0, z + v0);
            vertices[idx + 19] = new Vector3(x + v1, y + v0, z + v1);
            vertices[idx + 20] = new Vector3(x + v0, y + v0, z + v0); // face left
            vertices[idx + 21] = new Vector3(x + v0, y + v1, z + v0);
            vertices[idx + 22] = new Vector3(x + v0, y + v1, z + v1);
            vertices[idx + 23] = new Vector3(x + v0, y + v0, z + v1);
        }
        mesh.SetVertices(vertices);
        mesh.RecalculateBounds(); // boundsを計算しなおさないと再描画がおかしいので実行
    }
}