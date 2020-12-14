// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;

public class Heatmap : MonoBehaviour
{
    Vector4[] positions;
    Vector4[] properties;

    int count = 0;

    public Material material;

    public enum HeatmapType
    {
        Position,
        Death,
        Jump
    }

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
            properties[i] = new Vector4(1.0f, 0.5f, 0, 0);
        }

        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }

    private void Update()
    {
        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }

    public void GenerateMap(HeatmapType type)
    {
        switch (type)
        {
            case HeatmapType.Position:
                Debug.Log("HOLA");
                var dic = PlayerEventTrack.PositiontData;

                count = dic.Count;
                Debug.Log(count);

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                    properties[i] = new Vector4(1.0f, 0.5f, 0, 0);
                }

                material.SetInt("_Points_Length", count);
                material.SetVectorArray("_Points", positions);
                material.SetVectorArray("_Properties", properties);
                break;
            case HeatmapType.Death:
                break;
            case HeatmapType.Jump:
                break;
        }
    }
}