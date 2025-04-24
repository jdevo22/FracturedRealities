using System.Collections.Generic;
using UnityEngine;

public class BoatMover : MonoBehaviour
{
    public bool isEnabled = false; // start in off state

    [Header("Path Settings")]
    public Transform[] waypoints;
    public float speed = 2f;
    public float reachThreshold = 0.1f;
    public bool loop = false;

    [Header("Rocking Settings")]
    public float rockingAmplitude = 2f;     // degrees
    public float rockingFrequency = 1f;     // cycles per second

    private int currentWaypointIndex = 0;
    private Quaternion initialRotation;
    private Vector3 lastPosition;

    // Track players currently on the boat
    private HashSet<Transform> passengers = new HashSet<Transform>();
    

    void Start()
    {
        initialRotation = transform.rotation;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (isEnabled)
        {
            Vector3 currentPosition = transform.position;

            MoveAlongPath();
            ApplyRockingMotion();

            Vector3 boatDelta = transform.position - lastPosition;
            MovePassengers(boatDelta);

            lastPosition = transform.position;
        }

    }

    public void enableBoat()
    {
        isEnabled = true;
        return;
    }

    public void disableBoat()
    {
        isEnabled = false;
        return;
    }

    void MoveAlongPath()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(targetWaypoint.position);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length && loop)
            {
                currentWaypointIndex = 0; // loop
            }
            else
            {
                //isEnabled = false; // disable after reaching final waypoint
            }
        }

    }

    void ApplyRockingMotion()
    {
        float wave = Mathf.Sin(Time.time * rockingFrequency) * rockingAmplitude;
        Quaternion rockingRotation = Quaternion.Euler(wave, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = rockingRotation;
    }

    void MovePassengers(Vector3 delta)
    {
        foreach (var passenger in passengers)
        {
            if (passenger != null)
            {
                passenger.position += delta;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            passengers.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            passengers.Remove(other.transform);
        }
    }
}
