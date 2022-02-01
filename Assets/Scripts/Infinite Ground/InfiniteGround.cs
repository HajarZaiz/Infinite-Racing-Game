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
    
    //Infinite Plane Variables
    private int i = 0;
    private Vector3 topLeft, topRight, bottomLeft, bottomRight;

    //Objects destruction variables
    [SerializeField] private Transform ship;

    //PowerUps Variables
    [SerializeField] private GameObject scoreTorus;
    private int cntScore = 0;
    [SerializeField] private GameObject speedTorus;
    private int cntSpeed = 0;

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

        //Continuously destroy speed and score torus behind
        GameObject[] torus1 = GameObject.FindGameObjectsWithTag("ScoreTorus");
        foreach (GameObject t in torus1)
            if (ship.position.z > t.transform.position.z + 10)
                GameObject.Destroy(t);

        GameObject[] torus2 = GameObject.FindGameObjectsWithTag("SpeedTorus");
        foreach (GameObject t in torus2)
            if (ship.position.z > t.transform.position.z + 10)
                GameObject.Destroy(t);
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
        topLeft = new Vector3(-100, 0, 200 + i);
        topRight = new Vector3(100, 0, 200 + i);
        bottomLeft = new Vector3(-100, 0, i);
        bottomRight = new Vector3(100, 0, i);
        i = i + 200;

        //Chunks iteration variables
        float x_increment1 = 0;
        float x_increment2 = 200 / frequency;

        float z_increment1 = 0;
        float z_increment2 = 200 / frequency;
        var cellNumber = frequency * frequency;
        int rowNumber = 1;

        //Split the terrain to chunks and iterate over them
        for(int j = 1; j < cellNumber; j++)
        {
            //Check if the chunk needs to be skipped
            //Check if the row number is odd or even
            if(rowNumber%2 == 1)
            {
                //Check if at the end of the row
                if(j%frequency == 0)
                {
                    //Update Row
                    z_increment1 = z_increment1 + (200 / frequency);
                    z_increment2 = z_increment2 + (200 / frequency);
                    rowNumber = rowNumber + 1;

                    //Reset the chunk cell
                    x_increment1 = 0;
                    x_increment2 = 200 / frequency;
                }
                //Skip every second cell
                if(j%2 == 0)
                {
                    //Update the chunk cell
                    x_increment1 = x_increment1 + (200 / frequency);
                    x_increment2 = x_increment2 + (200 / frequency);
                    continue;
                }
            }
            else
            {
                //Check if at the end of the row
                if (j % frequency == 0)
                {
                    //Update Row
                    z_increment1 = z_increment1 + (200 / frequency);
                    z_increment2 = z_increment2 + (200 / frequency);
                    rowNumber = rowNumber + 1;

                    //Reset the chunk cell
                    x_increment1 = 0;
                    x_increment2 = 200 / frequency;
                }
                //Skip every even numbered cell
                if(j%2 == 1)
                {
                    //Update the chunk cell
                    x_increment1 = x_increment1 + (200 / frequency);
                    x_increment2 = x_increment2 + (200 / frequency);
                    continue;
                }
            }
  
            //Number of obstacles per cell
            for(int k = 0; k < density; k++)
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

            //Spawn powerups
            if (j % frequency == 0)
            {
                cntScore += 1;
                cntSpeed += 1;
                //Spawn powerups
                if (cntScore >= 5)
                {
                    var x_rand = Random.Range(topLeft.x + 30, topRight.x - 30);
                    var z_rand = Random.Range(bottomLeft.z + z_increment1, bottomLeft.z + z_increment2);
                    GameObject powerUp = Instantiate(scoreTorus, new Vector3(x_rand, ship.position.y, z_rand), Quaternion.identity);
                    cntScore = 0;
                }
                if (cntSpeed >= 15)
                {
                    var x_rand = Random.Range(topLeft.x + 30, topRight.x - 30);
                    var z_rand = Random.Range(bottomLeft.z + z_increment1, bottomLeft.z + z_increment2);
                    GameObject powerUp = Instantiate(speedTorus, new Vector3(x_rand, ship.position.y, z_rand), Quaternion.identity);
                    cntSpeed = 0;
                }
                
            }
        }
    }


}
