using UnityEngine;

public class McGuffinSocket : MonoBehaviour
{
    public GameObject allowedObject; // Assign the exact McGuffin GameObject in the Inspector
    public GameObject outlineEffect; // Optional: Glow effect object

    private GameObject placedObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == allowedObject) // Check if it's the correct object
        {
            if (placedObject == null) // If nothing is already placed
            {
                placedObject = other.gameObject;
                placedObject.transform.position = transform.position; // Snap to socket position
                placedObject.transform.rotation = transform.rotation; // Align rotation

                Rigidbody rb = placedObject.GetComponent<Rigidbody>();
                if (rb) rb.isKinematic = true; // Freeze movement

                if (outlineEffect) outlineEffect.SetActive(false); // Disable outline
                Debug.Log("McGuffin placed!");

                // Trigger event or next script
                TriggerEvent();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == placedObject)
        {
            placedObject = null;
            if (outlineEffect) outlineEffect.SetActive(true); // Reactivate outline
            Debug.Log("McGuffin removed!");
        }
    }

    void TriggerEvent()
    {
        Debug.Log("Puzzle completed! Triggering event...");
        // Call another script or activate an object here
    }
}
