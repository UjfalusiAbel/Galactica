using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Singleton
    {
        get
        {
            if(instance == null)
            {
                throw new System.NullReferenceException();
            }
            return instance;
        }
    }

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 100f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private Rigidbody rb;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float x;
        float z;
        float mouseX;
        float mouseY;

        x = Input.GetAxis("Horizontal") * speed;
        z = Input.GetAxis("Vertical") * speed;
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, 0f) );
        rb.velocity = (Quaternion.Euler(rotationX, rotationY, 0f) * new Vector3(x, 0f, z));
    }
}
