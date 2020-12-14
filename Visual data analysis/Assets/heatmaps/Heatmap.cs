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
        Jump,
        OpenMenu,
        Attack
    }

    void Start()
    {
        var dic = ReadData.Read("Position_data");

        count = dic.Count;

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

    public void ResetMap()
    {
        count = 0;

        positions = new Vector4[count];
        properties = new Vector4[count];

        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }

    public void GenerateMap(HeatmapType type)
    {
        var dic = PlayerEventTrack.PositionData;
        int d = 0;
        switch (type)
        {
            case HeatmapType.Position:
                count = dic.Count;

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                    properties[i] = new Vector4(1.0f, 0.5f, 0, 0);
                }
                break;
            case HeatmapType.Death:
                dic = PlayerEventTrack.EventData;

                count = dic.Count;

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    if ((string)dic[i]["Type"] == "Dead")
                    {
                        Debug.Log("YES");
                        ++d;
                        positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                        properties[i] = new Vector4(1.0f, 0.85f, 0, 0);
                    }
                }
                count = d;
                break;
            case HeatmapType.Jump:
                dic = PlayerEventTrack.EventData;

                count = dic.Count;

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    if ((string)dic[i]["Type"] == "Jump")
                    {
                        ++d;
                        positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                        properties[i] = new Vector4(1.0f, 0.85f, 0, 0);
                    }
                }
                count = d;
                break;
            case HeatmapType.OpenMenu:
                dic = PlayerEventTrack.EventData;

                count = dic.Count;

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    if ((string)dic[i]["Type"] == "OpenMenu")
                    {
                        ++d;
                        positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                        properties[i] = new Vector4(1.0f, 0.85f, 0, 0);
                    }
                }
                count = d;
                break;
            case HeatmapType.Attack:
                dic = PlayerEventTrack.EventData;

                count = dic.Count;

                positions = new Vector4[count];
                properties = new Vector4[count];

                for (int i = 0; i < count; ++i)
                {
                    if ((string)dic[i]["Type"] == "Attack")
                    {
                        ++d;
                        positions[i] = new Vector4((float)dic[i]["PositionX"], 0f, (float)dic[i]["PositionZ"], 0);
                        properties[i] = new Vector4(1.0f, 0.85f, 0, 0);
                    }
                }
                count = d;
                break;
        }
        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }
}