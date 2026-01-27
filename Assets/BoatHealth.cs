using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BoatHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHP = 3;
    public int currentHP;
    public HealthUI healthUI;
    public FailManager failManager;

    public float invincibleTime = 0.8f; // 无敌时间
    bool isInvincible = false;

    [Header("Rock Damage")]
    public float rockDamageSpeed = 10f; // 撞 Rock 掉血速度阈值

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;

        if (healthUI != null)
            healthUI.Init(maxHP, currentHP);
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (!GameState.IsGameStarted) return;
        // === 1️ Hammer：必掉 1 血 ===
        if (other.CompareTag("Hammer"))
        {
            LoseHP(1);
            Debug.Log("[HIT] Hammer  -1 HP");
            return;
        }
    }
*/
    void OnCollisionEnter(Collision collision)
    {
        if (!GameState.IsGameStarted) return;
        
        if (collision.gameObject.CompareTag("Rock")){
            float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= rockDamageSpeed)
        {
            LoseHP(1);
            Debug.Log($"[HIT] Rock speed={impactSpeed:F1} -1 HP");
        }
        else
        {
            Debug.Log($"[HIT] Rock speed={impactSpeed:F1} no damage");
        }
        }
          

        if (collision.gameObject.CompareTag("SpikeWall")){
            if(isInvincible) return;
            LoseHP(1);
            StartCoroutine(InvincibleRoutine());
            CheckpointManager.Instance.Respawn(rb);
        }

    }

IEnumerator InvincibleRoutine()
{
    isInvincible = true;

    // 可选：这里加视觉 / haptic / 音效
    // e.g. 闪烁、手柄震动

    yield return new WaitForSeconds(invincibleTime);

    isInvincible = false;
}

    void LoseHP(int amount)
    {
        if (currentHP <= 0)
            return;

        currentHP -= amount;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"Boat HP = {currentHP}/{maxHP}");

        healthUI.UpdateHP(currentHP);

        if (currentHP == 0)
        {
            OnDead();
        }
    }

    void OnDead()
    {
        Debug.Log("Boat destroyed");
        
        if (failManager != null)
        {
            failManager.ShowFail(); // 显示失败界面
        }
    }

    public void SetMaxHP(int newMaxHP, bool fill = true)
    {
        maxHP = newMaxHP;

        if (fill)
            currentHP = maxHP;
        else
            currentHP = Mathf.Min(currentHP, maxHP);

        if (healthUI != null)
            healthUI.Init(maxHP, currentHP);
    }

    public void AddMaxHP(int amount)
    {
        SetMaxHP(maxHP + amount);
    }
}
