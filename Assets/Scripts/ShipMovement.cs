using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float verticalMoveSpeed;
    [SerializeField] private float horizontalMoveSpeed;
    float speed = 10F;
    [SerializeField] private Vector3 directionv;
    [SerializeField] private Vector3 directionh;

    //Variables for components
    [SerializeField] private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Keep Ship within camera view
        clampWithinScreen();
    }

    void FixedUpdate()
    {
        //Basic Ship Movement
        Move();
    }

    private void Move()
    {
        //Movement along the x axis
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = 1;

        //Rotate the ship according to movement
        if(moveX > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-70, 90, -90), Time.deltaTime * speed);
        }
        else if(moveX < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-110, 90, -90), Time.deltaTime * speed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, -90), Time.deltaTime * speed);
        }
        //Direction of Movement
        //direction = new Vector3(moveX, 0, moveZ);
        directionv = new Vector3(moveX, 0, 0);
        directionh = new Vector3(0, 0, moveZ);
        //Move
        //controller.Move(direction * moveSpeed * Time.deltaTime);
        controller.Move(directionh * verticalMoveSpeed * Time.deltaTime);
        controller.Move(directionv * horizontalMoveSpeed * Time.deltaTime);

    }

    void clampWithinScreen()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }
}
