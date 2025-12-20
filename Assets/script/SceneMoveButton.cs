using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveButton : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    public void MoveScene()
    {
        // 현재 씬 이름 저장
        SceneHistory.PreviousSceneName =
            SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(targetSceneName);
    }
}