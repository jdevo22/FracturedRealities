using UnityEngine;

public class DoubleHingedDoor : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 5f;

    public Transform leftHinge;
    public Transform rightHinge;

    public Vector3 leftOpenRotation = new Vector3(0, -90f, 0);  // Opens outward
    public Vector3 rightOpenRotation = new Vector3(0, 90f, 0);  // Opens outward

    public float rotationSpeed = 2f;

    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    private Quaternion leftOpenQuat;
    private Quaternion rightOpenQuat;

    private void Start()
    {
        leftClosedRotation = leftHinge.localRotation;
        rightClosedRotation = rightHinge.localRotation;

        leftOpenQuat = Quaternion.Euler(leftOpenRotation);
        rightOpenQuat = Quaternion.Euler(rightOpenRotation);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        bool isPlayerClose = distance <= activationDistance;

        Quaternion targetLeft = isPlayerClose ? leftOpenQuat : leftClosedRotation;
        Quaternion targetRight = isPlayerClose ? rightOpenQuat : rightClosedRotation;

        leftHinge.localRotation = Quaternion.Slerp(leftHinge.localRotation, targetLeft, Time.deltaTime * rotationSpeed);
        rightHinge.localRotation = Quaternion.Slerp(rightHinge.localRotation, targetRight, Time.deltaTime * rotationSpeed);
    }
}
