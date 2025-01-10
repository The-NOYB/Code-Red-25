using UnityEngine;
using UnityEngine.EventSystems;

public class TilemapPanning : MonoBehaviour
{
    private Vector3 dragOrigin;

    private void Update()
    {
        // Detect left mouse button press
        if (GlobalVariables.selectMode == 0 && Input.GetMouseButtonDown(0))
        {
            // Store the initial mouse position when pressed
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragOrigin.z = 0;  // Set z to 0 to keep the tilemap 2D
        }

        // If the left mouse button is held down
        if (GlobalVariables.selectMode == 0 && Input.GetMouseButton(0))
        {
            // Calculate how much the mouse has moved since the initial click
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;

            // Calculate the delta movement (how much the mouse has moved)
            Vector3 delta = dragOrigin - currentPosition;

            // Reverse the delta to pan in the opposite direction
            transform.position -= delta;

            // Update the drag origin for the next frame
            dragOrigin = currentPosition;
        }
    }
}
