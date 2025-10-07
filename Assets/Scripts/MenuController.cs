using UnityEngine;

public class MenuController : MonoBehaviour
{
   public void PlayGame()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("TASK1_Beatty");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
