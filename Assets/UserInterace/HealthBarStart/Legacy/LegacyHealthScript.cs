using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyHealthScript : MonoBehaviour
{
    private int healthMax = 10;
    private int healthMin = 0;
    private int healthCurrent = 5;

    public float healthBarPosMax;
    public float healthBarPosMin;
    public GameObject healthBarObject;


    private void Awake()
    {
        OnHealthChange();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
        float healthPercent = Mathf.Lerp(healthBarPosMin, healthBarPosMax, healthRatio);

        healthBarObject.transform.localPosition = new Vector2(healthPercent, healthBarObject.transform.localPosition.y);

    }






}
