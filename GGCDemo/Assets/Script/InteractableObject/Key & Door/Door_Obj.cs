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
   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (ableToOpen == true)
        {
            Destroy(this.gameObject);

        }
    }
}
