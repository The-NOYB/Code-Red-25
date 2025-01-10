using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public GameObject block;

    public float panSpeed = 10f; // Speed of panning.
    private Vector3 dragOrigin; // Starting point of the drag.
    public Vector2 displacement; // Store x and y displacement.

    void Awake(){
        GenerateMap();
    }
    
    void GenerateMap()
    {
        for (int x=mapWidth; x>-1; x--)
        {
            for (int y=0; y<mapHeight; y++)
            {
                float x_off = (x+y)/2f;
                float y_off = (x-y)/4f;
                GameObject tile = Instantiate( block, new Vector3(x_off, y_off, 0), Quaternion.identity );
            }
        }
    }

   void Update()
   {
    CalculatePanDisplacement();
   } 

    void CalculatePanDisplacement()
    {
        // On mouse button down, set the drag origin.
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // If mouse button is not held, don't calculate.
        if (!Input.GetMouseButton(0)) return;

        // Calculate the displacement based on mouse movement.
        Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        displacement += new Vector2(-difference.x * panSpeed, -difference.y * panSpeed);

        // Update the drag origin.
        dragOrigin = Input.mousePosition;
    }

}