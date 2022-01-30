using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Object;
    bool touched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            Object.active = true;
            touched = true;
            Debug.Log("Hit");
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Object.active = false;
    }
}
