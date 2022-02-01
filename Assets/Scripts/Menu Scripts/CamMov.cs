using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMov : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0f, 0f, -1f), ForceMode.Force);
    }
}
