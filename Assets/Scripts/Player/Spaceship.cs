using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private float yawTorque = 50f;
    [SerializeField]
    private float pitchTorque = 100f;
    [SerializeField]
    private float rollTorque = 100f;
    [SerializeField]
    private float thrust = 100f;
    [SerializeField]
    private float upThrust = 100f;
    [SerializeField]
    private float strafeThrust = 50f;

    private Rigidbody rb;

    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    private bool isInputEnabled = true;

    private static Spaceship instance;
    public static Spaceship Singleton
    {
        get
        {
            return instance;
        }
    }

    public bool SetInputIsEnabled
    {
        set
        {
            isInputEnabled = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isInputEnabled)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        //Forgatás Z-tengelyen
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.fixedDeltaTime);
        //Forgatás X-tengelyen
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(pitchYaw.y, -1f, 1f) * pitchTorque * Time.fixedDeltaTime);
        //Forgatás Y-tengelyen
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.fixedDeltaTime);

        //Elore
        rb.AddRelativeForce(Vector3.forward * thrust1D * thrust * Time.fixedDeltaTime);
        //Fel-le
        rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
        //Oldalmozgas
        rb.AddRelativeForce(Vector3.right * strafe1D * strafeThrust * Time.fixedDeltaTime);
    }

    #region Bemenetek

    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    #endregion
}
