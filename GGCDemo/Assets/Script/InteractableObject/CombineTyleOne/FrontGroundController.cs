using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontGroundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Deactivate() {
        CombineHelper[] helperList = GetComponentsInChildren<CombineHelper>();
        foreach (CombineHelper helper in helperList) {
            helper.Deactive();
        }
    }
}
