using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerManager : MonoBehaviour
{

    public bool printerFixed = false;

    public void OnPrinterFixed()
    {
        printerFixed = true;
    }

    public void Teleport()
    {
        if (printerFixed)
        {
            SceneManager.LoadScene(1);
        }
    }

}
