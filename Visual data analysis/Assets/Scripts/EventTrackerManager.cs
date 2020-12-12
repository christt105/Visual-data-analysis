using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrackerManager : SaveData
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            Debug.Log("hello");
            SaveEvents();
            SavePosition();
        }
    }

    private void OnApplicationQuit()
    {
        //Writte xml/json/csv
       
       // Save();

    }
}
