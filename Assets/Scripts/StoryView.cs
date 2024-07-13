using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StoryView : MonoBehaviour
{
    [SerializeField] private RectTransform choiceHolder;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject normalHudGroup;

    [SerializeField] private List<SpeakerConfig> speakerConfigs;

    private UnityAction _onFinished;

    [Serializable]
    public class SpeakerConfig
    {
        public string name;
        public Sprite sprite;
    }

    private Story story;
    private List<IQuest> _quests;
    private PlayerInput _playerInput;

    private void Awake()
    {
        DestroyOldChoices();
        gameObject.SetActive(false);
        _playerInput = FindObjectOfType<PlayerInput>();
        
        CollectionQuest[] collectionQuests = Resources.LoadAll<CollectionQuest>("Quests");
        _quests = new List<IQuest>();
        foreach (var collectionQuest in collectionQuests)
        {
            _quests.Add(collectionQuest);
        }
    }

    public void StartStory(TextAsset textAsset, UnityAction onFinished)
    {
        _onFinished = onFinished;
        normalHudGroup.SetActive(false);
        _playerInput.currentActionMap = _playerInput.actions.FindActionMap("UI");
        gameObject.SetActive(true);
        story = new Story(textAsset.text);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        foreach (var quest in GameState.GetCompletedQuests())
        {
            var varName = "completed" + quest.Quest.GetId().ToLower();
            if (story.variablesState.Contains(varName))
            {
                story.variablesState["completed" + quest.Quest.GetId().ToLower()] = true;
            }
        }
        foreach (var quest in GameState.GetCompletableQuests())
        {
            var varName = "completed" + quest.Quest.GetId().ToLower();
            if (story.variablesState.Contains(varName))
            {
                story.variablesState["completable" + quest.Quest.GetId().ToLower()] = true;
            }
        }

        foreach (var quest in GameState.GetActiveQuests())
        {
            var varName = "completed" + quest.Quest.GetId().ToLower();
            if (story.variablesState.Contains(varName))
            {
                story.variablesState["active" + quest.Quest.GetId().ToLower()] = true;
            }
        }

        ShowStory();
    }

    private void CloseStory()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
        normalHudGroup.SetActive(true);
        _playerInput.currentActionMap = _playerInput.actions.FindActionMap("Player");
        _onFinished?.Invoke();
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
            CreateContentView(text); // Display the text on screen!
            HandleTags(); // For example: give new quests
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

    private void HandleTags()
    {
        if (story.currentTags.Count <= 0)
        {
            return;
        }

        foreach (var currentTag in story.currentTags)
        {
            if (currentTag.Contains("addQuest"))
            {
                var questName = currentTag.Split(' ')[1];
                var quest = _quests.First(q => q.GetId().ToLower() == questName.ToLower());
                GameState.StartQuest(quest);
                FindObjectOfType<QuestLogView>(true).ShowActiveQuests();
            }

            if (currentTag.Contains("removeQuest"))
            {
                var questName = currentTag.Split(' ')[1];
                GameState.RemoveQuest(questName);
                FindObjectOfType<QuestLogView>(true).ShowActiveQuests();
            }

            if (currentTag.Contains("completeQuest"))
            {
                var questName = currentTag.Split(' ')[1];
                GameState.CompleteQuest(questName);
                FindObjectOfType<QuestLogView>(true).ShowActiveQuests();
            }
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
        StartCoroutine(ShowTextLetterByLetter(text));
    }

    IEnumerator ShowTextLetterByLetter(string text)
    {
        storyText.text = text;
        storyText.maxVisibleCharacters = 0;
        for (int i = 0; i <= text.Length; i++)
        {
            storyText.maxVisibleCharacters = i;
            if (_playerInput.actions["Skip"].WasPressedThisFrame())
            {
                storyText.maxVisibleCharacters = text.Length;
                yield break;
            }

            yield return new WaitForSeconds(0.015f); // wir könnten auch 1 sekunde warten, das wäre sehr langsam
        }
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
        }

        choice.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce).From(0f).SetDelay(index * 0.2f);

        var choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        return choice;
    }
}