using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        // 위에서 내려와서 착지했을 때만
        if (collision.relativeVelocity.y <= 0f)
        {
            GameEndManager manager = FindAnyObjectByType<GameEndManager>();
            if (manager != null)
                manager.EndGame();
        }
    }
}