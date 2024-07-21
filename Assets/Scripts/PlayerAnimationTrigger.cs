using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private HashSet<Collider> nearbyPlayers = new HashSet<Collider>();
    public TextMeshProUGUI interactionText;

    public Animator playerAnimator;
    public string playerTriggerName = "PlayerInteract";

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (nearbyPlayers.Count > 0 && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit")))
        {
            TriggerPlayerAnimation();
            DestroyPickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearbyPlayers.Add(other);
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearbyPlayers.Remove(other);
            if (nearbyPlayers.Count == 0 && interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    private void TriggerPlayerAnimation()
    {

        if (playerAnimator != null && !string.IsNullOrEmpty(playerTriggerName))
        {
            playerAnimator.SetTrigger(playerTriggerName);
        }
    }

    private void DestroyPickup()
    {

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }


        foreach (var player in nearbyPlayers)
        {
            OnTriggerExit(player);
        }
        nearbyPlayers.Clear();


        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}