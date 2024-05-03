using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class StoryView : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    private Story story;

    [SerializeField] private RectTransform choiceHolder;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Button buttonPrefab;

    [SerializeField] private Image speakerImage;

    [SerializeField] private List<SpeakerConfig> speakerConfigs;

    [Serializable]
    public class SpeakerConfig
    {
        public string name;
        public Sprite sprite;
    }

    private void Awake()
    {
        DestroyOldChoices();
        gameObject.SetActive(false);
    }

    public void StartStory(TextAsset textAsset)
    {
        FindObjectOfType<PlayerInput>().enabled = false;
        gameObject.SetActive(true);
        story = new Story(textAsset.text);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ShowStory();
    }

    private void CloseStory()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
        FindObjectOfType<PlayerInput>().enabled = true;
    }

    private void ShowStory()
    {
        DestroyOldChoices();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
            // HandleTags(); //TODO: tags kommen später
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim(), i);
                // Tell the button what to do when we press it
                button.onClick.AddListener(() => OnClickChoiceButton(choice));
            }
        }
        else
        {
            Button choice = CreateChoiceView("Continue", 0);
            choice.onClick.AddListener(CloseStory);
        }
    }

    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        ShowStory();
    }

    private void CreateContentView(string text)
    {
        var speaker = story.globalTags.FirstOrDefault(t => t.Contains("speaker"))?.Split(' ')[1];
        speakerName.text = speaker;
        speakerImage.sprite = GetSpeakerImage(speaker);
        StartCoroutine(ShowTextLetterByLetter(text));
    }
    
    
    private Sprite GetSpeakerImage(string speaker)
    {
        return speakerConfigs.FirstOrDefault(s => s.name == speaker)?.sprite;
    }

    private IEnumerator ShowTextLetterByLetter(string text)
    {
        storyText.text = text;
        storyText.maxVisibleCharacters = 0;
        for (int i = 0; i <= text.Length; i++)
        {
            storyText.maxVisibleCharacters = i;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                storyText.maxVisibleCharacters = text.Length;
            }
        }

        yield return new WaitForSeconds(0.025f);
    }

private void DestroyOldChoices()
    {
        foreach (Transform child in choiceHolder)
        {
            Destroy(child.gameObject);
        }
    }
    
    private Button CreateChoiceView(string text, int index)
    {
        var choice = Instantiate(buttonPrefab, choiceHolder.transform, false);
        if (index == 0)
        {
            choice.Select();
            choice.navigation = new Navigation { mode = Navigation.Mode.Vertical, selectOnDown = choice };
        }

        choice.transform
            .DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
            .From(0f)
            .SetDelay(index * 0.2f);
        
        var choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        return choice;
    }
}