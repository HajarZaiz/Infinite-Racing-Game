using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{

    [SerializeField] private GameObject ground;
    [SerializeField] private Vector3 spawnNext;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGround();
        SpawnGround();
    }

    public void SpawnGround()
    {
        GameObject g = Instantiate(ground, spawnNext, Quaternion.identity);
        spawnNext = g.transform.GetChild(1).transform.position;
    }

}
