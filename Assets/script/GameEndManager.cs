using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] string resultSceneName;

    public void EndGame()
    {
        PlayerPrefs.SetFloat("PlayTime", timer.GetCurrentTime());
        PlayerPrefs.Save();

        SceneManager.LoadScene(resultSceneName);
    }
}