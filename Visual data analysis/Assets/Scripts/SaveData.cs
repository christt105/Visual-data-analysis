using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;


public class SaveData : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();


    public void SaveEvents()
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Type";
        rowDataTemp[1] = "TimeStamp";
        rowDataTemp[2] = "PositionX";
        rowDataTemp[3] = "PositionY";
        rowDataTemp[4] = "PositionZ";
        rowDataTemp[5] = "PlayerID";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < PlayerEventTrack.EventList.Count; i++)
        {
            if (PlayerEventTrack.EventList[i].TryGetValue("Type", out object value))
            {
                if (value != "Move")
                {
                    rowDataTemp = new string[PlayerEventTrack.EventList[i].Count];
                    int index = 0;
                    foreach (var j in PlayerEventTrack.EventList[i])
                    {
                        rowDataTemp[index++] = j.Value.ToString(); // type
                    }
                    rowData.Add(rowDataTemp);
                    
                    
                }
            }
           
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath("Saved_data");

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SavePosition()
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Type";
        rowDataTemp[1] = "TimeStamp";
        rowDataTemp[2] = "PosX";
        rowDataTemp[3] = "PosY";
        rowDataTemp[4] = "PosZ";
        rowDataTemp[5] = "PlayerID";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < PlayerEventTrack.EventList.Count; i++)
        {
            if (PlayerEventTrack.EventList[i].TryGetValue("Type", out object value))
            {
                if (value == "Move")
                {
                    rowDataTemp = new string[PlayerEventTrack.EventList[i].Count];
                    int index = 0;
                    foreach (var j in PlayerEventTrack.EventList[i])
                    {
                        rowDataTemp[index++] = j.Value.ToString(); // type
                    }
                    rowData.Add(rowDataTemp);


                }
            }

        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath("Position_data");

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(string name)
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + name + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}