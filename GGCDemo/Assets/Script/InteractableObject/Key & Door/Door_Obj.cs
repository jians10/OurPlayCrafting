using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Obj : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ableToOpen;
    void Start()
    {
        ableToOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToOpen == true)
        {
            Debug.Log("hit");

            Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
