using UnityEngine;

public class GameStateHandler : MonoSingleton<GameStateHandler>
{

    public GameObject player;

    public delegate void OnGamePause();
    public event OnGamePause onGamePause;

    public delegate void OnGameContinue();
    public event OnGameContinue onGameContinue;

    /// <summary>
    ///  Works when player just interact with some other objects. Not the actual Pause Game
    /// </summary>
    /// <returns> OnGamePuse,OnGameContinue,etc.. Methods</returns>
    public void PauseGame()
    {
        //TODO: oyun pause edildiðinde oyuncuyu pause edilme animasyonuna sok--> mesela idle veya static bir idle hal
        
        //player.GetComponent<Animator>().enabled = false;

        onGamePause?.Invoke();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        onGameContinue?.Invoke();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
