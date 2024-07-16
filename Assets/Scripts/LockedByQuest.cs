using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedByQuest : MonoBehaviour
{
    [SerializeField] private CollectionQuest quest;

    public CollectionQuest Quest => quest;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
