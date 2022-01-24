using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineHelper : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject backObject;
    private GameObject frontObject;
    private Transform frontLayer;
    private Transform backLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFrontChild(GameObject target) {
        target.transform.parent = transform;
        frontObject = target;
    }
    public void SetBackChild(GameObject target) {
        target.transform.parent = transform;
        backObject = target;
    }
    public void SetBackLayer(Transform layer) {
        backLayer = layer;
    }
    public void SetFrontLayer(Transform layer){
        frontLayer = layer;
    }

    public void Deactive() {
        backObject.transform.parent = backLayer;
        frontObject.transform.parent = frontLayer;
        Destroy(this.gameObject);
    }


    
}
