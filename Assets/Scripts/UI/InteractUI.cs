using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] GameObject uiElement;

    PlayerInteraction interaction;

    private void Start()
    {
        uiElement.SetActive(false);
        interaction = FindObjectOfType<PlayerInteraction>();
        interaction.OnInteractStateChange += Interaction_OnInteractStateChange;
    }

    private void Interaction_OnInteractStateChange(bool value)
    {
        uiElement.SetActive(value);
    }

    private void OnDestroy()
    {
        interaction.OnInteractStateChange -= Interaction_OnInteractStateChange;
    }
}
