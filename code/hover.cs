using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    public float hoverAmount = 0.5f; // How much to hover
    public float hoverSpeed = 2.0f;  // Speed of the hover
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Hover up and down using sine wave
        float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
        transform.position = startPosition + new Vector3(0, newY, 0);
    }
}
