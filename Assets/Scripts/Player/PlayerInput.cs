using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float rotationSpeed = 10f;

    private Rigidbody rb;
    private float movementX;
    private float movementZ;
    private Vector2 rotation;
    private bool enableMovement = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(enableMovement)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        rb.AddRelativeForce(transform.forward * -1f * movementX * speed);
        rb.AddRelativeForce(transform.right * -1f * movementZ * speed);
        transform.Rotate(0f, rotation.x, 0f);
    }

    public void OnMovementX(InputAction.CallbackContext context)
    {
        movementX = context.ReadValue<float>();
    }

    public void OnMovementZ(InputAction.CallbackContext context)
    {
        movementZ = context.ReadValue<float>();
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        enableMovement = true;
    }
}
