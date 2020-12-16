using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace Adhaesii.WazoooDOTexe
{
    public class GameManager : SerializedMonoBehaviour
    {
        public void Quit()
        {
            print("quit");
            #if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying) UnityEditor.EditorApplication.isPlaying = false;
            #else
            UnityEngine.Application.Quit();
            #endif
        }

        public void Restart()
        {
            print("restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
