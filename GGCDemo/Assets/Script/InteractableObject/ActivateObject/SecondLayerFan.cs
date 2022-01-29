using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLayerFan : SecondLayerObject
{
    // Start is called before the first frame update
    private Fan myfan;
    override public void Start()
    {
        myfan = GetComponent<Fan>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public void active()
    {
        //Debug.Log("activated fan");
        myfan.activate();
    }
    override public void deactive()
    {
        //Debug.Log("deactivated fan");
        //base.deactive();
        myfan.deactivate();
    }
}
