using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CamMov : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform t;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject spawnRing;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject btns;
    [SerializeField] private GameObject ship;
    private Image bg;
    private bool pressed;
    private bool spawn;
    private Vector3 initialScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        spawnRing.SetActive(false);
        spawn = false;
        menuCanvas.SetActive(false);
        btns.SetActive(false);
        initialScale = new Vector3(1f, 1f, 1f);
        ship.transform.localScale = Vector3.zero;
        bg = menuCanvas.GetComponent<Image>();
        Color temp = bg.color;
        temp.a= 0f;
        bg.color = temp;
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

                menuCanvas.SetActive(true);
                spawn = true;
                StartCoroutine(SpawnRingCoroutine());
            }
        }
    }

    private IEnumerator SpawnRingCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeImage());
        spawnRing.SetActive(true);
        StartCoroutine(ScaleUpShip(5f));
        btns.SetActive(true);
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

    IEnumerator FadeImage()
    {
        for (float i = 0; i <= 0.5f; i += Time.deltaTime)
        {
            bg.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

}
