using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    public void PLayGAme()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
