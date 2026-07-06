using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            input.y = 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            input.y = -1;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            input.x = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            input.x = 1;

        Vector3 movement = new Vector3(input.x, input.y, 0f).normalized;

        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            bool stillAlive = GameManager.Instance.LoseLife();

            if (stillAlive)
            {
                transform.position = startPosition;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("SafetySign"))
        {
            GameManager.Instance.CollectSign();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.TryWin();
        }
    }
}