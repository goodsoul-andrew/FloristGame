using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlsScript : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }
}