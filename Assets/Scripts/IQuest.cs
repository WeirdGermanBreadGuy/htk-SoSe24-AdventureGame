using UnityEngine;
using UnityEngine.Rendering;

public interface IQuest
{
    public string GetId();
    public bool IsHidden();
    string GetDisplayName();
    GameObject GetCompleteScreenPrefab();
}
