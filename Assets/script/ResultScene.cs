using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    [SerializeField] string restartSceneName;

    void Start()
    {
        string name = PlayerPrefs.GetString("PlayerName", "플레이어");
        float time = PlayerPrefs.GetFloat("PlayTime", 0f);

        int min = (int)(time / 60);
        int sec = (int)(time % 60);
        int ms  = (int)((time * 100) % 100);

        resultText.text =
            $"{name}님의 기록은 {min:00}:{sec:00}.{ms:00}!!";
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(restartSceneName);
    }
}