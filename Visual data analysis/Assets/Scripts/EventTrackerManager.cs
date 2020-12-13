using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrackerManager : SaveData
{
    void Awake()
    {

        PlayerEventTrack.EventData = ReadData.Read("Saved_data");

        for (var i = 0; i < PlayerEventTrack.EventData.Count; i++)
        {
            print("Type " + PlayerEventTrack.EventData[i]["Type"] + " " +
                   "TimeStamp " + PlayerEventTrack.EventData[i]["TimeStamp"] + " " +
                   "PositionX " + PlayerEventTrack.EventData[i]["PositionX"] + " " +
                   "PositionY " + PlayerEventTrack.EventData[i]["PositionY"]);
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
