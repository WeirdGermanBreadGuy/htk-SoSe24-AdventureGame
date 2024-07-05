using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class UiService
{
    public static GameObject Open(GameObject screen)
    {
        var uiRoot = Object.FindObjectOfType<UIRoot>();
        if (uiRoot == null)
        {
            throw new Exception("No UiRoot found in scene");
        }
        
        return Object.Instantiate(screen, uiRoot.transform);
    }
}