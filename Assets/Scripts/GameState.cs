using System.Collections.Generic;
using UnityEngine;
    public class GameState : MonoBehaviour
    {
        private Dictionary<ItemType, uint> items = new();

        public static void AddItem(ItemType type, uint amount)
        {
            var instance = FindObjectOfType<GameState>();

            if (!instance.items.TryAdd(type, amount))
            {
                instance.items[type] += amount;
            }
        }

        public static bool TryRemoveItem(ItemType type, uint amount)
        {
            var instance = FindObjectOfType<GameState>();
            if ( instance.items.TryGetValue(type, out var ownedAmount))
            {
                if (ownedAmount < amount)
                {
                    return false; // pech gehabt, nicht genug von type
                }

                instance.items[type] -= amount; // Transaktion
                return true; // Cool
            }
            
            return false; // Meh
        }
    }
