using UnityEngine;

public class StunBlock : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject stunEffectPrefab;
    public float spawnInterval = 4f;

    [Header("Effect Height")]
    public float effectHeight = 20f; // ⭐ Inspector에서 조절

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEffect();
            timer = 0f;
        }
    }

    void SpawnEffect()
    {
        Vector3 spawnPos = transform.position;

        GameObject effect = Instantiate(
            stunEffectPrefab,
            spawnPos,
            Quaternion.identity
        );

        StunEffect se = effect.GetComponent<StunEffect>();
        se.minY = transform.position.y;
        se.maxY = transform.position.y + effectHeight;
    }
}