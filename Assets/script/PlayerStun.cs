using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    bool isStunned;
    float stunTimer;

    PlayerMove move;

    void Awake()
    {
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (!isStunned)
            return;

        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0f)
        {
            isStunned = false;
            move.SetStun(false);
        }
    }

    public void ApplyStun(float time)
    {
        if (time <= 0f)
            return;

        isStunned = true;
        stunTimer = time;
        move.SetStun(true);
    }
}