using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [System.Serializable]
    public class PotionCombination
    {
        public string potionTagA;
        public string potionTagB;
        public GameObject resultPotionPrefab;
    }

    public List<PotionCombination> combinations;

    private string firstPotionTag = null;
    private string secondPotionTag = null;

    public Transform spawnPoint; // Where the new potion will appear

    public void RegisterPotion(string potionTag)
    {
        if (firstPotionTag == null)
        {
            firstPotionTag = potionTag;
        }
        else if (secondPotionTag == null)
        {
            secondPotionTag = potionTag;
            TryCombinePotions();
        }
    }

    public void UnregisterPotion(string potionTag)
    {
        if (firstPotionTag == potionTag)
            firstPotionTag = null;
        else if (secondPotionTag == potionTag)
            secondPotionTag = null;
    }

    private void TryCombinePotions()
    {
        foreach (var combo in combinations)
        {
            bool match =
                (combo.potionTagA == firstPotionTag && combo.potionTagB == secondPotionTag) ||
                (combo.potionTagA == secondPotionTag && combo.potionTagB == firstPotionTag);

            if (match)
            {
                Instantiate(combo.resultPotionPrefab, spawnPoint.position, Quaternion.identity);
                ClearPotionSlots();
                return;
            }
        }

        // Optional: feedback for invalid combination
        Debug.Log("Invalid combination: " + firstPotionTag + " + " + secondPotionTag);
        ClearPotionSlots();
    }

    private void ClearPotionSlots()
    {
        firstPotionTag = null;
        secondPotionTag = null;
    }
}