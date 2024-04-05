using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class StoryView : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI storyText;

   [SerializeField] 
   private Button closeButton;

   private Story _story;

   private void Awake()
   {
      gameObject.SetActive(false);
      closeButton.onClick.AddListener(CloseStory);
   }

   public void StartStory(TextAsset story)
   {
      FindObjectOfType<PlayerInput>().enabled = false;
      gameObject.SetActive(true);
      _story = new Story(story.text);
      storyText.text = _story.Continue();
      Debug.Log(story.text);
      closeButton.Select();
   }
    
   public void CloseStory()
   {
      FindObjectOfType<PlayerInput>().enabled = true;
      gameObject.SetActive(false);
   }  
}
