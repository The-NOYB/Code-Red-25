    using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityTracker : MonoBehaviour
{
    // UI Elements
    public Text statusText;          // Displays GPS status
    public Text activityText;        // Displays activity type (Walking/Cycling)
    public Text distanceText;        // Displays total distance traveled

    private Vector2 startPosition;   // GPS starting position
    private Vector2 currentPosition; // GPS current position
    private bool tracking = false;   // GPS tracking status
    private float totalDistance = 0; // Total distance traveled in meters

    void Start()
    {
        StartCoroutine(StartGPS());  // Initialize GPS
    }

    IEnumerator StartGPS()
    {
        if (!Input.location.isEnabledByUser)
        {
            statusText.text = "GPS not enabled. Please enable location services.";
            yield break;
        }

        Input.location.Start(); // Start the GPS service

        // Wait until GPS initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            statusText.text = "Unable to determine device location.";
            yield break;
        }
        else
        {
            statusText.text = "GPS activated!";
            startPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            tracking = true; // Start tracking
        }
    }

    void Update()
    {
        if (tracking)
        {
            TrackActivity(); // Detect activity type using motion sensors
            TrackDistance(); // Calculate distance using GPS
        }
    }

    void TrackActivity()
    {
        // Get accelerometer data
        Vector3 acceleration = Input.acceleration;

        // Detect walking or cycling based on motion patterns
        if (acceleration.magnitude > 0.3f && acceleration.magnitude < 1.5f)
        {
            activityText.text = "Walking Detected!";
        }
        else if (acceleration.magnitude >= 1.5f)
        {
            activityText.text = "Cycling Detected!";
        }
        else
        {
            activityText.text = "Stationary";
        }
    }

    void TrackDistance()
    {
        // Update the current GPS position
        currentPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // Calculate distance traveled since the last frame
        float distance = CalculateDistance(startPosition, currentPosition);

        // Update the total distance
        totalDistance += distance;

        // Display the total distance
        distanceText.text = "Distance: " + totalDistance.ToString("F2") + " meters";

        // Update the start position for the next frame
        startPosition = currentPosition;
    }

    float CalculateDistance(Vector2 start, Vector2 end)
    {
        float R = 6371e3f; // Radius of the Earth in meters
        float lat1 = start.x * Mathf.Deg2Rad;
        float lat2 = end.x * Mathf.Deg2Rad;
        float deltaLat = (end.x - start.x) * Mathf.Deg2Rad;
        float deltaLon = (end.y - start.y) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
                  Mathf.Cos(lat1) * Mathf.Cos(lat2) *
                  Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float distance = R * c; // Distance in meters
        return distance;
    }

    void OnDestroy()
    {
        Input.location.Stop(); // Stop the GPS service when the app is closed
    }
}