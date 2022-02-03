using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMov : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform t;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject spawnRing;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject ship;
    private bool pressed;
    private bool spawn;
    private Vector3 initialScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        spawnRing.SetActive(false);
        spawn = false;
        menuCanvas.SetActive(false);
        initialScale = new Vector3(1f, 1f, 1f);
        ship.transform.localScale = Vector3.zero;
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
        else if(transform.position.z <= -9)
        {
            rb.Sleep();
            if (!spawn)
            {
                spawn = true;
                StartCoroutine(SpawnRingCoroutine());
            }
        }
    }

    private IEnumerator SpawnRingCoroutine()
    {
        yield return new WaitForSeconds(3);
        spawnRing.SetActive(true);
        StartCoroutine(ScaleUpShip(5f));
        menuCanvas.SetActive(true);
    }

    private IEnumerator ScaleUpShip(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 scaleFrom = Vector3.zero;
        Vector3 scaleTo = initialScale;
        while(i < 1)
        {
            i += Time.deltaTime * rate;
            ship.transform.localScale = Vector3.Lerp(scaleFrom, scaleTo, i);
            yield return 0;
        }
    }

}
