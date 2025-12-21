using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingBackButton : MonoBehaviour
{
    public void GoBack()
    {
        if (!string.IsNullOrEmpty(SceneHistory.PreviousSceneName))
        {
            SceneManager.LoadScene(SceneHistory.PreviousSceneName);
        }
    }
}