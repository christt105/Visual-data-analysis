// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;

public class Heatmap1 : MonoBehaviour
{
    public Vector4[] positions;
    public Vector4[] properties;

    public Material material;

    public int count = 15;

    void Start()
    {
        positions = new Vector4[count];
        properties = new Vector4[count];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector4(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0, 0);
            properties[i] = new Vector4(1.5f, 0.5f, 0, 0);
        }
    }

    void Update()
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += new Vector4(Random.Range(-0.1f, +0.1f), Random.Range(-0.1f, +0.1f), 0, 0) * Time.deltaTime;

        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }
}