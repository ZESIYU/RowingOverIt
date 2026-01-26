using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    [Header("FinishEvents")]
    public UnityEvent onFinish;

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Finish");
            // 触发事件
            onFinish?.Invoke();
    }
}
