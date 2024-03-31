using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractbleObject : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject panel;
    

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {

    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }

    void ClosePanel()
    {
        panel.SetActive(false);
    }
    
    void IInteractable.Interact()
    {
        ShowPanel();
    }

    void IInteractable.UnInteract()
    {
        ClosePanel();
    }
}
