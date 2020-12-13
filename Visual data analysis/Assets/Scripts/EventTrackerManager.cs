using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrackerManager : SaveData
{
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

    // UI Events ====================================================
    public void ButtonReadDate()
    {
        PlayerEventTrack.EventData = ReadData.Read("Saved_data");
    }
    // ==============================================================

}
