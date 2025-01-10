using UnityEngine;
using UnityEngine.UI;

public class TaskProgressbar : MonoBehaviour
{
    private Button[] buttons;

    void Start()
    {
        // Find all buttons named "Button1" through "Button7" dynamically
        buttons = new Button[7];
        for (int i = 0; i < 7; i++)
        {
            string buttonName = "Button" + (i + 1); // Button1, Button2, etc.
            GameObject buttonObject = GameObject.Find(buttonName);

            if (buttonObject != null)
            {
                buttons[i] = buttonObject.GetComponent<Button>();
                int capturedIndex = i; // Capture current index
                buttons[i].onClick.AddListener(() => OnButtonClick(buttons[capturedIndex]));
            }
            else
            {
                Debug.LogError($"Button {buttonName} not found in the scene!");
            }
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        if (clickedButton != null)
        {
            clickedButton.gameObject.SetActive(false); // Hide the clicked button
        }
        else
        {
            Debug.LogError("Clicked button is null!");
        }
    }
}
