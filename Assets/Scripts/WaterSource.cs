using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WaterSource : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemType _requiredItem;
    [SerializeField] private bool _shouldConsume;
    [SerializeField] private uint requiredAmount;
    public ItemType type;
    public uint amount = 1;
     public void Interact()
    {
        if (_shouldConsume)
        {
            if (GameState.TryRemoveItem(_requiredItem, requiredAmount))
            {
                FillVase();
            }
        }
        else
        {
            if (GameState.HasEnoughItems(_requiredItem, requiredAmount))
            {
                FillVase();
            }
        }
    }

    private void FillVase()
    {
        GameState.AddItem(type, amount);
        Debug.Log("Collected");
    }
}
