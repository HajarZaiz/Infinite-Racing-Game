using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDestroy : MonoBehaviour
{
    InfiniteGround infiniteGround;
    // Start is called before the first frame update
    void Start()
    {
        infiniteGround = GameObject.FindObjectOfType<InfiniteGround>();
    }

    private void OnTriggerExit(Collider other)
    {
        infiniteGround.SpawnGround();
        //Destroy the previous plane
        Destroy(gameObject, 1);
    }
}
