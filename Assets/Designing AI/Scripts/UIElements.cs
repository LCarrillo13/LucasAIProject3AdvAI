using UnityEngine;
using UnityEngine.SceneManagement;

namespace LucasAIP
{
    public class UIElements : MonoBehaviour
    {
        public void QuitButton()
        {
            Time.timeScale = 1;

            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
        public void RetryButton()
        {
            Time.timeScale = 1;
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}