using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [SerializeField] float movSpeed = 15f;

    [Header("Bounds Settings")]
    [SerializeField] Vector2 min;
    [SerializeField] Vector2 max;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0, moveZ);

        transform.Translate(direction * movSpeed * Time.deltaTime, Space.World);

        float clampedX = Mathf.Clamp(transform.position.x, min.x, max.x);
        float clampedZ = Mathf.Clamp(transform.position.z, min.y, max.y);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
