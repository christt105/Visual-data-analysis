using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrackerManager : SaveData
{
    void Awake()
    {

        List<Dictionary<string, object>> data = ReadData.Read("Saved_data");

        for (var i = 0; i < data.Count; i++)
        {
            print("Type " + data[i]["Type"] + " " +
                   "TimeStamp " + data[i]["TimeStamp"] + " " +
                   "PositionX " + data[i]["PositionX"] + " " +
                   "PositionY " + data[i]["PositionY"]);
        }

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            Debug.Log("Saving Data into .CSV File...");
            SaveEvents();
            SavePosition();
        }
    }

    
    private void OnApplicationQuit()
    {
        SavePosition();
        SaveEvents();
    }
}
