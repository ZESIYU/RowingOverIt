using UnityEngine;

public class TestMoveObject : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    void Update()
    {
        if (!GameState.IsGameStarted)
            return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}