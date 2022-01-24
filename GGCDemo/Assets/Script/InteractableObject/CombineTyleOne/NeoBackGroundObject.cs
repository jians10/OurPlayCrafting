using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoBackGroundObject: MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject combination;
    public Collider2D[] Mycolliders;
    //public GameObject FrontObject;
    void Start()
    {
        if (Mycolliders.Length==0) {

            Mycolliders = GetComponentsInChildren<Collider2D>();
        }
    }

    public void deactivateObject() {
        Debug.Log("called");
        foreach (Collider2D Mycollider in Mycolliders) {
            Mycollider.enabled = false;
        }
        
    }
    public void activateObject() {
        foreach (Collider2D Mycollider in Mycolliders)
        {
            Mycollider.enabled = true;
        }
    }
    



}
