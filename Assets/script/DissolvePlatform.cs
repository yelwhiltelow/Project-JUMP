using System.Collections;
using UnityEngine;

public class DissolvePlatform : MonoBehaviour
{
    [Header("Delay Settings")]
    [SerializeField] float minDisappearDelay = 1f;
    [SerializeField] float maxDisappearDelay = 3f;

    [Header("Respawn Settings")]
    [SerializeField] float respawnTime = 10f;

    Collider2D col;
    SpriteRenderer sr;

    bool isTriggered = false;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTriggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // 위에서 밟았을 때만 작동
            if (collision.contacts[0].normal.y < -0.5f)
            {
                isTriggered = true;
                StartCoroutine(DissolveRoutine());
            }
        }
    }

    IEnumerator DissolveRoutine()
    {
        float delay = Random.Range(minDisappearDelay, maxDisappearDelay);
        yield return new WaitForSeconds(delay);

        // 소멸
        col.enabled = false;
        sr.enabled = false;

        // 리젠 대기
        yield return new WaitForSeconds(respawnTime);

        // 리젠
        col.enabled = true;
        sr.enabled = true;
        isTriggered = false;
    }
}