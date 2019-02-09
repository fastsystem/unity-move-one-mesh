using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManyObject : MonoBehaviour
{
    private List<GameObject> cubes;

    void OnEnable()
    {
        cubes = new List<GameObject>();
        for (int x = -50; x < 50; x++)
        {
            for (int z = -50; z < 50; z++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, 0, z);
                cubes.Add(cube);
            }
        }
    }

    void OnDisable()
    {
        foreach(var cube in cubes)
            GameObject.Destroy(cube);
        cubes.Clear();
    }

    void Update()
    {
        if (cubes.Count == 0) return;

        var idx = 0;
        for (int x = -50; x < 50; x++)
        {
            for (int z = -50; z < 50; z++)
            {
                var y = Mathf.Sin(Mathf.Sqrt(x * x + z * z) / 4 + Time.realtimeSinceStartup) * 3;
                cubes[idx].transform.position = new Vector3(x, y, z);
                idx++;
            }
        }
    }
}
