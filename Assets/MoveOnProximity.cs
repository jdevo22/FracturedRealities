using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnProximity : MonoBehaviour
{
    public Transform player;                // Assign your player transform in the Inspector
    public float activationDistance = 5f;   // Distance at which the object moves up

    public Vector3 upPositionOffset = new Vector3(0, 2f, 0);   // How far up it goes
    public Vector3 downPositionOffset = Vector3.zero;          // Down position relative to start

    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 upPosition;
    private Vector3 downPosition;

    private void Start()
    {
        startPosition = transform.position;
        upPosition = startPosition + upPositionOffset;
        downPosition = startPosition + downPositionOffset;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 targetPosition = distance <= activationDistance ? upPosition : downPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
