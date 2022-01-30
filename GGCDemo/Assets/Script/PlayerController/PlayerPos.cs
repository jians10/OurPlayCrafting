using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    //private CheckpointSystem cs;

    void Start()
    {
        //cs = GameObject.FindGameObjectWithTag("CS").GetComponent<CheckpointSystem>();
        //transform.position = cs.lastCheckPointPos;
        transform.position = CheckpointSystem.instance.lastCheckPointPos;
    }

    void Update()
    {
    }
}
