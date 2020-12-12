using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrack : MonoBehaviour
{

    static public List<Dictionary<string, object>> EventList = new List<Dictionary<string, object>>();

    private void OnApplicationQuit()
    {
        //Writte xml/json/csv
    }

}
