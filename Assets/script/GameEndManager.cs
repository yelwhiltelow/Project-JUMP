using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] string resultSceneName;

    public void EndGame()
    {
        // 1️⃣ 플레이 시간 가져오기
        float playTime = timer.GetCurrentTime();

        // 2️⃣ 이름 가져오기 (시작 씬에서 저장한 것)
        string playerName = PlayerPrefs.GetString("PlayerName", "플레이어");

        // 3️⃣ 결과 씬에서 쓰기 위해 시간 저장
        PlayerPrefs.SetFloat("PlayTime", playTime);

        // ⭐ 4️⃣ 랭킹 데이터에 추가 (이 줄이 핵심)
        RankManager.AddRank(playerName, playTime);

        // 5️⃣ 저장
        PlayerPrefs.Save();

        // 6️⃣ 결과 씬으로 이동
        SceneManager.LoadScene(resultSceneName);
    }
}