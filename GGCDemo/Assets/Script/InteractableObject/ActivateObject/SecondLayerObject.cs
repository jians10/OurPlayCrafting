using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLayerObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isSecondGround;
    virtual public void Start()
    {
        //if (gameObject.layer == LayerMask.NameToLayer("hidden")||gameObject.layer== LayerMask.NameToLayer("hiddenground")) {
        if (isSecondGround)
        {
            deactive();
        }
        else {
            active();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void active()
    {

        
        return;
    }
    virtual public void deactive() {


        return;
    }
}
