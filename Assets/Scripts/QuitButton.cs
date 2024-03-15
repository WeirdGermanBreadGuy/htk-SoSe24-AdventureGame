using System;
using UnityEngine;
using UnityEngine.UI;


public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
