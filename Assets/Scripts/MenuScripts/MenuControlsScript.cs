using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlsScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        //Debug.Log("Exiting game");
        Application.Quit();
    }
}