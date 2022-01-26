using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show() {
        gameObject.layer = 8;
    }
    public void hide() {
        gameObject.layer = 9; 
    }




}
