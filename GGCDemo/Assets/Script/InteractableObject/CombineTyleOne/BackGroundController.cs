using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // Start is called before the first frame update
    private BackGroundObject[] objectList;
    public bool Active;
    void Start()
    {
       
        objectList = GetComponentsInChildren<BackGroundObject>();
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void Activate() {
        Active = true;
        foreach (BackGroundObject obj in objectList) {
            obj.activateObject();
        }
    }
    public void Deactivate() {
        Active = false;
        foreach (BackGroundObject obj in objectList) {
            Debug.Log("deactivate");
            obj.deactivateObject();
        }
    }
}
