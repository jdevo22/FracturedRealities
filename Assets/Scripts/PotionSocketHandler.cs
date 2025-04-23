using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PotionSocketHandler : MonoBehaviour
{
    public PotionManager potionManager;

    private XRSocketInteractor socketInteractor;
    private string currentPotionTag;

    void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnPotionPlaced);
        socketInteractor.selectExited.AddListener(OnPotionRemoved);
    }

    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnPotionPlaced);
        socketInteractor.selectExited.RemoveListener(OnPotionRemoved);
    }

    private void OnPotionPlaced(SelectEnterEventArgs args)
    {
        GameObject potion = args.interactableObject.transform.gameObject;
        currentPotionTag = potion.tag;

        if (potionManager != null && !string.IsNullOrEmpty(currentPotionTag))
        {
            potionManager.RegisterPotion(currentPotionTag);
        }
    }

    private void OnPotionRemoved(SelectExitEventArgs args)
    {
        if (potionManager != null && !string.IsNullOrEmpty(currentPotionTag))
        {
            potionManager.UnregisterPotion(currentPotionTag);
        }

        currentPotionTag = null;
    }
}
