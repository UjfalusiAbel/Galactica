using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockToStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ship")
        {
            Rigidbody ship = other.GetComponent<Rigidbody>();
            ship.velocity = Vector3.zero;
            ship.angularVelocity = Vector3.zero;
            Spaceship.Singleton.SetInputIsEnabled = false;
        }
    }
}
