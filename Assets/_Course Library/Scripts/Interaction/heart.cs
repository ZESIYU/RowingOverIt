using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        Debug.Log("吃到爱心了");

            // 这里可以加回血、加分等逻辑
            // 比如：other.GetComponent<PlayerHP>().Heal(10);

        Destroy(gameObject); // 爱心消失
        
    }
}
