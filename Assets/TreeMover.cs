using UnityEngine;

public class TreeMover : MonoBehaviour
{
    [Header("Target Settings")]
    public float targetZPosition = 286f;
    public float targetZRotation = 0f; // in degrees

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 50f;

    private bool movementComplete = false;

    void Update()
    {
        if (!movementComplete)
        {
            RotateToTargetZ();
            MoveToTargetZ();

            // Check if both position and rotation targets are reached
            if (Mathf.Approximately(transform.position.z, targetZPosition) &&
                Mathf.Approximately(NormalizeAngle(transform.eulerAngles.z), NormalizeAngle(targetZRotation)))
            {
                movementComplete = true;
            }
        }
    }

    void RotateToTargetZ()
    {
        float currentZ = transform.eulerAngles.z;
        float step = rotationSpeed * Time.deltaTime;
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetZRotation, step);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
    }

    void MoveToTargetZ()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetZPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    float NormalizeAngle(float angle)
    {
        return (angle + 360f) % 360f;
    }
}
