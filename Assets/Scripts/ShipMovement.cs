using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float verticalMoveSpeed;
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float maxVerticalMoveSpeed;
    [SerializeField] private int speedPickUpTime;
    [SerializeField] private int speedPickUpTime2;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationSpeed2;
    [SerializeField] float slowMoSpeed;
    [SerializeField] private Vector3 directionv;
    [SerializeField] private Vector3 directionh;
    private Transform myTransform;
    private Vector3 lastPosition;
    private bool isMoving;

    //Variables for components
    [SerializeField] private CharacterController controller;

    //Variables for collision
    [SerializeField] private GameObject explosion;

    int prev = 0;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Keep Ship within camera view
        clampWithinScreen();

        //Movement along the x axis
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = 1;

        //Rotate the ship according to movement
        if (moveX > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-70, 90, -90), Time.deltaTime * rotationSpeed);
        }
        else if (moveX < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-110, 90, -90), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90, 90, -90), Time.deltaTime * rotationSpeed2);
        }
    }

    void FixedUpdate()
    {
        //Basic Ship Movement
        //Move();
    }

    private void Move()
    {
        //Movement along the x axis
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = 1;

        
        //Direction of Movement
        directionv = new Vector3(moveX, 0, 0);
        directionh = new Vector3(0, 0, moveZ);
        //Move
        controller.Move(directionh * verticalMoveSpeed * Time.deltaTime);
        controller.Move(directionv * horizontalMoveSpeed * Time.deltaTime);
        SpeedAdjustment();
        CheckMoving();
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

    
    void OnCollisionEnter(Collision collision)
    {
        //Explosion on contact with obstacle
        if (collision.gameObject.tag == "Obstacle")
        {
            //Add Explosion Effect
            Instantiate(explosion, transform.position, transform.rotation);
            //Destroy the Ship
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //PowerUps Collisions
        if (collision.gameObject.tag == "ScoreTorus")
        {
            UIManager.instance.ScoreBoost();
            Destroy(collision);
        }

        if (collision.gameObject.tag == "SpeedTorus")
        {
            Destroy(collision);
            float initialSpeed = verticalMoveSpeed;
            verticalMoveSpeed = slowMoSpeed;
            StartCoroutine(ResumeSpeed(initialSpeed));
        }
    }

    private IEnumerator ResumeSpeed(float initialSpeed)
    {
        yield return new WaitForSeconds(5);

        verticalMoveSpeed = initialSpeed;
    }

    //Increase speed every speedPickUpTime seconds of real gameplay
    private void SpeedAdjustment()
    {
        float time = (int) Time.realtimeSinceStartup;
        if (time != prev && time % speedPickUpTime == 0)
        {
            //Clamp speed
            if(verticalMoveSpeed < maxVerticalMoveSpeed)
            {
                verticalMoveSpeed += 1;
            }
            prev = (int) time;
        }
    }

    //Fix for when stuck between obstacles and not destroyed
    private void CheckMoving(){
        if ( myTransform.position != lastPosition)
            isMoving = true;
        else{
            isMoving = false;
            Instantiate(explosion, transform.position, transform.rotation);
        }
        lastPosition = myTransform.position;
    }

}
