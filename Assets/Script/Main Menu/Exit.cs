using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    public void QuitGame()
    {
        // If running in the Unity editor
        Application.Quit();
    }
}