using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        Vector3 position = transform.position;

        if (Keyboard.current.rightArrowKey.isPressed)
            position.x += moveSpeed * Time.deltaTime;

        if (Keyboard.current.leftArrowKey.isPressed)
            position.x -= moveSpeed * Time.deltaTime;

        if (Keyboard.current.upArrowKey.isPressed)
            position.y += moveSpeed * Time.deltaTime;

        if (Keyboard.current.downArrowKey.isPressed)
            position.y -= moveSpeed * Time.deltaTime;

        // Limit camera position
        position.x = Mathf.Max(position.x, -1000f);
        position.y = Mathf.Max(position.y, 0f);

        transform.position = position;
    }

    void Start()
    { 
        transform.position = new Vector3(600, 50, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0); 
        transform.localScale = Vector3.one;
    }
}