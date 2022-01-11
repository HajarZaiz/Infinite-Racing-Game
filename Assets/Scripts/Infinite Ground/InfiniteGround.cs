using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    //Ground Spawning Variables
    [SerializeField] private GameObject ground;
    [SerializeField] private Vector3 spawnNext;

    //Obstacles Spawning Variable
    //Plane dimensions
    [SerializeField] private float plane_x;
    [SerializeField] private float plane_z;
    [SerializeField] private GameObject obstaclePrefab;
    //Corners of plane
    Vector3[] vertices;
    private int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        SpawnGround();
        SpawnGround();
    }

    public void SpawnGround()
    {
        //Spawn the plane at the next position
        GameObject g = Instantiate(ground, spawnNext, Quaternion.identity);
        spawnNext = g.transform.GetChild(1).transform.position;
        
        //Global coordinates of plane corners
        Vector3 topLeft = new Vector3(-100, 0, 200 + i);
        Vector3 topRight = new Vector3(100, 0, 200 + i);
        Vector3 bottomLeft = new Vector3(-100, 0, i);
        Vector3 bottomRight = new Vector3(100, 0, i);
        i = i + 200;

        for (var j = 0; j < 1000; j++)
        {
            //Randomize Spawning position
            var x_rand = Random.Range(topLeft.x, topRight.x);
            var z_rand = Random.Range(bottomLeft.z, topLeft.z);

            //Randomize Y scale


            //Spawn Obstacle
            GameObject obs = Instantiate(obstaclePrefab, new Vector3(x_rand, 0.5f, z_rand), Quaternion.identity);
        }

    }

}
