using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractbleObject : MonoBehaviour, IInteractable
{

    KeyCode interactKey = KeyCode.E;
    [SerializeField] private GameObject showcasePanel;

    public string infoText;
    public string objectName;

    public bool canInteract;
    public event Action<bool> canShowInfo;

    private void Awake()
    {
        canShowInfo += CanShowInfo;
    }
    private void OnDestroy()
    {
        canShowInfo -= CanShowInfo;
    }

    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(interactKey))
            {
                ShowPanel();
            }

        }
    }

    public void ShowPanel()
    {
        GameStateHandler.Instance.PauseGame();
        showcasePanel.SetActive(true);
        showcasePanel.transform.DOScale(1, 0.2f);
    }

    public void ClosePanel()
    {
        GameStateHandler.Instance.ContinueGame();
        showcasePanel.transform.DOScale(0, 0.2f).OnComplete(() =>
        {
            showcasePanel.SetActive(false);
        });

    }

    void CanShowInfo(bool canShow)
    {
        if (canShow)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }

    void IInteractable.Interact()
    {
        canShowInfo?.Invoke(true);
    }

    void IInteractable.UnInteract()
    {
        canShowInfo?.Invoke(false);
    }
}
