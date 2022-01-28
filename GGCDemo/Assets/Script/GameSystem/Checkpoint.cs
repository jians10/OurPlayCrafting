using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointSystem cs;

    void Start()
    {
        cs = GameObject.FindGameObjectWithTag("CS").GetComponent<CheckpointSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log(transform.position);
            cs.lastCheckPointPos = transform.position;
        }
    }
}
