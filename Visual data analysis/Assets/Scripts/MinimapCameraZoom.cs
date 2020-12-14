using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraZoom : MonoBehaviour
{
    Camera cam;
    public float scale;
    public float speed;
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if(Gamekit3D.StartUI.m_InPause)
        {

            if (cam.orthographicSize >= 10 && cam.orthographicSize <= 150)
            {
                float size = cam.orthographicSize;
                size -= Input.mouseScrollDelta.y * scale;
                cam.orthographicSize = size;
            }
            if (cam.orthographicSize < 10)
                cam.orthographicSize = 10;
            if (cam.orthographicSize > 150)
                cam.orthographicSize = 150;


            Vector3 move_pos = cam.transform.localPosition;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                move_pos = new Vector3(move_pos.x, move_pos.y, move_pos.z - speed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                move_pos = new Vector3(move_pos.x, move_pos.y, move_pos.z + speed);

            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                move_pos = new Vector3(move_pos.x - speed, move_pos.y, move_pos.z);

            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                move_pos = new Vector3(move_pos.x + speed, move_pos.y, move_pos.z);
            }

            // Update final pos
            cam.transform.localPosition = move_pos;
        }
    }
}
