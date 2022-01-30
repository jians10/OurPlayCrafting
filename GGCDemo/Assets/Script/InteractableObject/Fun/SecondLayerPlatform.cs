using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLayerPlatform : SecondLayerObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public void active()
    {
        gameObject.layer = LayerMask.NameToLayer("ground");
        Debug.Log("toGround");
    }
    override public void deactive()
    {
        gameObject.layer = LayerMask.NameToLayer("hiddenground");
        Debug.Log("toHidden");
    }
}
