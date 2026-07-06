using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 3f;
    public int direction = 1;

    public float leftLimit = -5f;
    public float rightLimit = 5f;

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (direction == 1 && transform.position.x > rightLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }

        if (direction == -1 && transform.position.x < leftLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
        }
    }
}