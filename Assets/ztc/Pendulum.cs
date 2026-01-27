using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed = 1f;
    public float limit = 75f;
    public bool randomStart = true;

    float random = 0f;
    bool rotateOnX = false; // true = X轴摆，false = Z轴摆

    void Awake()
    {
        if (randomStart)
            random = Random.Range(0f, 1f);

        // 只在一开始判断一次
        float yAngle = transform.localRotation.y;
        if (yAngle > 180f) yAngle -= 360f;

        rotateOnX = Mathf.Abs(yAngle) > 0.01f;
    }

    void Update()
    {
        float angle = limit * Mathf.Sin((Time.time + random) * speed);

        if (rotateOnX)
        {
            transform.localRotation = Quaternion.Euler(0f, 90f, angle);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
