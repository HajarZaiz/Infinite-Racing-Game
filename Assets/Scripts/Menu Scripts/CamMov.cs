using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMov : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform t;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    private bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            pressed = true;
        }
        if (transform.position.z > -9 && pressed)
        {
            panel1.SetActive(false);
            rb.AddForce(new Vector3(0f, 0f, -1f), ForceMode.Force);
        }
        else
        {
            rb.Sleep();
        }
    }
}
