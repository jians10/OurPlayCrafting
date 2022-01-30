using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Obj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetPoint;
    void Start()
    {
        //targetPoint = GameObject.Find("TestDoor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerAdvance")
		{
            targetPoint.GetComponent<Door_Obj>().ableToOpen = true;
            Destroy(this.gameObject);

        }
    }
}
