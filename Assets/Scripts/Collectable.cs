using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class Collectable : MonoBehaviour, IInteractable
{
   public ItemType type;
   public uint amount = 1;

   public void Interact()
   {
      Debug.Log("Collected" + name);

      GameState.AddItem(type, amount);


   }
}
