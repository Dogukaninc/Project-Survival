using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{

    public GameObject player;

    public static GameStateHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        } 
    }

    public void PauseGame()
    {
        player.GetComponent<CharacterAiming>().enabled = false;
        player.GetComponent<CharacterLocomotion>().enabled = false;
        player.GetComponent<ActiveWeapon>().enabled = false;
        player.GetComponent<ReloadWeapon>().enabled = false;
        //player.GetComponent<Animator>().enabled = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void ContinueGame()
    {
        player.GetComponent<CharacterAiming>().enabled = true;
        player.GetComponent<CharacterLocomotion>().enabled = true;
        player.GetComponent<ActiveWeapon>().enabled = true;
        player.GetComponent<ReloadWeapon>().enabled = true;
        //player.GetComponent<Animator>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
