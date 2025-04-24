using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolKitHealthBar : MonoBehaviour
{
    private int maxHealthBarPer = 81;
    private int minHealthBarPer = 12;

    private int healthMax = 10;
    private int healthCurrent = 5;

    public UIDocument UiDoc;
    private VisualElement healthBarMask;

    private void Start()
    {
        healthBarMask = UiDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthCurrent--;
            OnHealthChange();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            healthCurrent++;
            OnHealthChange();
        }


    }


    private void OnHealthChange()
    {
        float healthRatio = (float)healthCurrent / healthMax;
        float healthPercent = Mathf.Lerp(minHealthBarPer, maxHealthBarPer, healthRatio);

        healthBarMask.style.width = Length.Percent(healthPercent);
    }

}
