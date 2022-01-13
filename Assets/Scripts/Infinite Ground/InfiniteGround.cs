using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    //Ground Spawning Variables
    [SerializeField] private GameObject ground;
    [SerializeField] private Vector3 spawnNext;

    //Obstacles Spawning Variable
    //----------------------Number of cells per row----------------------------
    [SerializeField] [Range(5, 20)] private int frequency;
    //----------------------Obstacles Height interval----------------------------
    [SerializeField] private int heightMin;
    [SerializeField] private int heightMax;
    //----------------------Number of obstacles per cell----------------------------
    [SerializeField] private float density;
    [SerializeField] private GameObject obstaclePrefab;
    //Corners of plane
    private Vector3[] vertices;
    private int i = 0;
    private float z_pos;

    //Obstacles destruction variables
    [SerializeField] private Transform ship;


    // Start is called before the first frame update
    void Start()
    {
        SpawnGround();
        SpawnGround();
        SpawnGround();
    }

    void Update()
    {
        //Continuously destroy obstacles behind
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject o in obstacles)
            if(ship.position.z > o.transform.position.z + 10)
                GameObject.Destroy(o);
    }

    public void SpawnGround()
    {
        //Spawn the plane at the next position
        GameObject g = Instantiate(ground, spawnNext, Quaternion.identity);
        spawnNext = g.transform.GetChild(1).transform.position;

        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        //Global coordinates of plane corners
        Vector3 topLeft = new Vector3(-100, 0, 200 + i);
        Vector3 topRight = new Vector3(100, 0, 200 + i);
        Vector3 bottomLeft = new Vector3(-100, 0, i);
        Vector3 bottomRight = new Vector3(100, 0, i);
        i = i + 200;

        //Chunks iteration variables
        float x_increment1 = 0;
        float x_increment2 = 200 / frequency;

        float z_increment1 = 0;
        float z_increment2 = 200 / frequency;
        var cellNumber = frequency * frequency;

        //Split the terrain to chunks (Initially split to frequency*frequency chunks that increase size with difficulty)
        for(var j = 1; j < cellNumber; j++)
        {
            //Number of obstacles per cell
            for (var k = 0; k < density; k++)
            {
                //Randomize Spawning position in that chunk
                var x_rand = Random.Range(topLeft.x + x_increment1, topLeft.x + x_increment2);
                var z_rand = Random.Range(bottomLeft.z + z_increment1, bottomLeft.z + z_increment2);

                //Randomize Y scale
                var y_rand = Random.Range(heightMin, heightMax);

                //Spawn Obstacle
                GameObject obs = Instantiate(obstaclePrefab, new Vector3(x_rand, y_rand / 2, z_rand), Quaternion.identity);
                obs.transform.localScale = new Vector3(9, y_rand, 1);
            }

            //Update the chunk cell
            x_increment1 = x_increment1 + (200 / frequency);
            x_increment2 = x_increment2 + (200 / frequency);

            //Move to the next row if needed
            if (j % frequency == 0)
            {
                z_increment1 = z_increment1 + (200 / frequency);
                z_increment2 = z_increment2 + (200 / frequency);

                //Reset the chunk cell
                x_increment1 = 0;
                x_increment2 = 200 / frequency;
            }

        }
    }


}
