using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleScenes : MonoBehaviour
{
    public void RestartSteakScene()
    {
        SceneManager.LoadScene("SteakScene");
    }

    public void ServeSteakScene()
    {
        SceneManager.LoadScene("ServeSteakScene");
    }
}
