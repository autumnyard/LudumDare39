using UnityEngine;

public class InfiniteRotation : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0f, 0f, 1 * speed * Time.deltaTime);
    }
}
