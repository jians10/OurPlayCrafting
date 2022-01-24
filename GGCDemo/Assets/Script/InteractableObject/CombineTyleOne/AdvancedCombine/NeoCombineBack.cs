using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoCombineBack : CombineBack
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void Combine(GameObject target)
    {
        Debug.Log("Combine is triggering");
        //combine = Instantiate(combineHelper, transform.position, Quaternion.identity);
        combineHelper = new GameObject("CombineHelper");
        combineHelper.transform.position = target.transform.position;
        //combineHelper.AddComponent<CombineHelper>();
        //CombineHelper helper = combineHelper.GetComponent<CombineHelper>();
        combineHelper.transform.parent = frontgrondLayer;
        Destroy(target.GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Rigidbody2D>());
        target.transform.parent=combineHelper.transform;
        transform.parent = combineHelper.transform;
        combineHelper.AddComponent<Rigidbody2D>();
        combineHelper.AddComponent<CompositeCollider2D>();
        //combineHelper.GetComponent<CompositeCollider2D>();
        //helper.SetBackChild(gameObject);
        //helper.SetFrontChild(target);
        //helper.SetFrontLayer(frontgrondLayer);
        //helper.SetBackLayer(backgroundLayer);
    }



    override public void Dectect() {

        //Vector2 DetectCenterFall = transform.position;
        Collider2D[] list = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);

        GameObject combineTarget = null;
        foreach (Collider2D c in list)
        {
            if (c.transform.parent && c.transform.parent.gameObject.tag == "CombineTypeOne")
            {
                Debug.Log("target Found ");
                combineTarget = c.transform.parent.gameObject;
                break;
            }
        }
        if (combineTarget != null)
        {

            Combine(combineTarget);
        }


        Debug.Log(list.Length);


    }


}
