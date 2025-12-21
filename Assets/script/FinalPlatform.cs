using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 위에서 밟았을 때만 작동
            if (collision.contacts[0].normal.y < -0.5f)
            {
                GameEndManager manager = FindAnyObjectByType<GameEndManager>();
                if (manager != null)
                    manager.EndGame();
            }
        }
    }
}