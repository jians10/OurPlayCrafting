using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public static CheckpointSystem instance;

    public Vector2 lastCheckPointPos;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 getCheckPointLocation() {
        return lastCheckPointPos;
    }
}
