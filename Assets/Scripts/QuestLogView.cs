using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestLogView : MonoBehaviour
{
    [SerializeField] private RectTransform questHolder;
    [SerializeField] private QuestStatusView questViewPrefab;

    public void ShowActiveQuests()
    {
        foreach (Transform child in questHolder)
        {
            Destroy(child.gameObject);
        }

        var activeQuests = GameState.GetActiveQuests();
        foreach (var quest in activeQuests)
        {
            if (quest.Status == GameState.QuestStatus.Completed)
            {
                continue;
            }
            
            var questView = Instantiate(questViewPrefab, questHolder);
            questView.Set(quest.Quest);
        }
    }
}