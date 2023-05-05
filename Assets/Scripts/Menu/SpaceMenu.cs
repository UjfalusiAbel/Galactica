using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject planetLanding;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.tag);
        if(collision.collider.gameObject.tag == "Planet")
        {
            Rigidbody ship = GetComponent<Rigidbody>();
            ship.velocity = Vector3.zero;
            ship.angularVelocity = Vector3.zero;
            Spaceship.Singleton.SetInputIsEnabled = false;
            planetLanding.SetActive(true);
        }
    }
}
