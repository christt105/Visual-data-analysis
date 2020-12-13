// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;

public class Heatmap : MonoBehaviour
{
    public Vector4[] positions;
    public Vector4[] properties;

    public Material material;

    public int count = 50;

    void Start()
    {
        var dic = ReadData.Read("Position_data");

        count = dic.Count;
        Debug.Log(count);

        positions = new Vector4[count];
        properties = new Vector4[count];

        for (int i = 0; i < count; ++i)
        {
            positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
            properties[i] = new Vector4(1.5f, 1.5f, 0, 0);
        }

        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }

    void Update()
    {
        //for (int i = 0; i < positions.Length; i++)
        //    positions[i] += new Vector4(Random.Range(-0.1f, +0.1f), Random.Range(-0.1f, +0.1f), 0, 0) * Time.deltaTime;

    }
}