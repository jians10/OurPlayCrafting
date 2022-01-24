using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public BackGroundController background;
    public FrontGroundController frontground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("Button C was pressed");
            if (!background.Active)
            {
                background.Activate();
            }
            else {
                frontground.Deactivate();
                background.Deactivate();
            }
        }
    }
}
