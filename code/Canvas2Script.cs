using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas2 : MonoBehaviour
{
    private Dictionary<string, int> buttonStates = new Dictionary<string, int>(); // Dictionary to track button states
    public int initialLength; // Total number of buttons at the start
    public int currentLength; // Number of buttons currently in the dictionary

    void Start()
    {
        InitializeButtonStates(); // Initialize button states in Start
    }

    // Function to initialize the dictionary for button states
    private void InitializeButtonStates()
    {
        // Find all Button components under this Canvas
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Get the button's name
            bool isActive = button.gameObject.activeSelf; // Check if the button is active

            // Add the button's state to the dictionary
            buttonStates[buttonName] = isActive ? 1 : -1;

            Debug.Log($"Button '{buttonName}' is {(isActive ? "active" : "inactive")}.");
        }

        // Set the initial length and the current length of the dictionary
        initialLength = buttonStates.Count - 3; // for the 3 scene changing buttons
        currentLength = initialLength;

        Debug.Log($"Button states initialized. Initial Length: {initialLength}, Current Length: {currentLength}");
    }


public void DeactivateButton(string buttonName)
{
    // Find the button by its name and deactivate it
    Button button = GameObject.Find(buttonName)?.GetComponent<Button>();

    if (button != null)
    {
        button.gameObject.SetActive(false); // Deactivate the button
        currentLength -= 1; // Decrement the current length
        buttonStates[buttonName] = -1; // changing the active 
        Debug.Log($"Button '{buttonName}' deactivated. Current length: {currentLength}, button active or not: {buttonStates[buttonName]}");
    }
    else
    {
        Debug.LogError($"Button '{buttonName}' not found!");
    }
}

}
