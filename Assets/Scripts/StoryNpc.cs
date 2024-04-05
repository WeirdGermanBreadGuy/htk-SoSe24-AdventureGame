using UnityEngine;

public class StoryNpc : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset story;

    public void Interact()
    {
        var storyView = FindObjectOfType<StoryView>(includeInactive: true);
        if (storyView.isActiveAndEnabled)
        {
            return;
        }

        storyView.StartStory(story);
    }
}

