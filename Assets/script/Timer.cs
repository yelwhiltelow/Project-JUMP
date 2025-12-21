using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    float curTime;

    int minute;
    int second;
    int millisecond;

    bool isRunning = false;

    private void Awake()
    {
        curTime = 0f;
        UpdateText();
    }

    void Update()
    {
        // 아직 시작 안 했을 때: 플레이어 입력 감지
        if (!isRunning)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 ||
                Input.GetButtonDown("Jump"))
            {
                isRunning = true;
            }
            else
            {
                return;
            }
        }

        // 타이머 진행
        curTime += Time.deltaTime;
        UpdateText();
    }

    void UpdateText()
    {
        minute = (int)(curTime / 60);
        second = (int)(curTime % 60);

        // 1/100초 단위 (00 ~ 99)
        millisecond = (int)((curTime * 100) % 100);

        text.text =
            minute.ToString("00") + ":" +
            second.ToString("00") + "." +
            millisecond.ToString("00");
    }
    
    public float GetCurrentTime()
    {
        return curTime;
    }
}