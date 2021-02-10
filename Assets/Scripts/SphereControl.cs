using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{

    private Rigidbody pRigid;
    // Start is called before the first frame update
    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            pRigid.AddForce(Vector3.forward, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            pRigid.AddForce(-Vector3.forward, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pRigid.AddForce(-Vector3.right, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pRigid.AddForce(Vector3.right, ForceMode.Impulse);
        }

        if(pRigid.velocity.magnitude > 5)
        {
            pRigid.velocity = pRigid.velocity.normalized;
            pRigid.velocity *= 5;
        }
    }
}
