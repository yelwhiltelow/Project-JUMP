using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] string gameSceneName;

    public void OnClickStart()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
            return;

        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameSceneName);
    }
}