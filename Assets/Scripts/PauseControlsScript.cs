using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControlsScript : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
