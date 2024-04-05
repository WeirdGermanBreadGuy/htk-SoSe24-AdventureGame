using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class StoryView : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI storyText;

   private Story _story;

   private void Awake()
   {
      gameObject.SetActive(false);
   }

   public void StartStory(TextAsset story)
   {
      gameObject.SetActive(true);
      _story = new Story(story.text);
      storyText.text = _story.Continue();
      Debug.Log(story.text);
   }
}
