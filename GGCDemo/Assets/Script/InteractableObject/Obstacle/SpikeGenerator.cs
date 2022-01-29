using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject spikeShowPrefab;
    public GameObject spikeHidePrefab;
    public int spikenumHigh=3;
    public int spikenumLow = 1;
    public float period;
    private float generateTimer;
    void Start()
    {
        generateTimer = Time.time + period;
    }

    // Update is called once per frame
    void Update()
    {
        //createSpike();
        //StartCoroutine(CreateSpike());
        if (generateTimer < Time.time) {

            generateTimer = Time.time + period;
            StartCoroutine(CreateSpike());
        
        }
    }


    void createSpike() {

        //Vector2 generationpoint= new Vector2(Random.Range(transform.position.x-transform.localScale.x/2, transform.position.x + transform.localScale.x / 2), transform.position.y);

        //spike.layer = gameObject.layer;
        //spike.transform.position = generationpoint;
        
    }

    IEnumerator CreateSpike() {
        int spikenum = (int)Random.Range(spikenumLow, spikenumHigh);

        for (int i = 0; i < spikenum; i++)
        {
            Vector2 generationpoint = new Vector2(Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2), transform.position.y);

            //spike.layer = LayerMask.NameToLayer("normalSpike");

            if (gameObject.layer == LayerMask.NameToLayer("hidden") || gameObject.layer == LayerMask.NameToLayer("hiddenground"))
            {
                Instantiate(spikeHidePrefab, generationpoint, Quaternion.identity);
            }
            else
            {
                Instantiate(spikeShowPrefab, generationpoint, Quaternion.identity);
            }
            //spike.layer = gameObject.layer;
            yield return new WaitForSeconds(0.1f);
        }
    }




}
