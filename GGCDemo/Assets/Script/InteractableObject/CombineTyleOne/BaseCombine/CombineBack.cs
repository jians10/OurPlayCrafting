using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineBack : MonoBehaviour
{
    private BackGroundObject myObject=null;
    //private bool Combine=false;
    public int NumLimit=0;
    public Transform backgroundLayer;
    public Transform frontgrondLayer;
    public GameObject combineHelper = null;


    // Start is called before the first frame update
    void Start()
    {
        myObject = GetComponentInParent<BackGroundObject>();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Find combine target 's trigger is still working");
    //    if (collision.gameObject.GetComponentInParent<CombineFront>())
    //    {
    //        if (!Combine)
    //        {
    //            collision.gameObject.GetComponentInParent<CombineFront>().Combine(myObject.gameObject);
    //            Combine = true;
    //        }
    //    }
    //}

    virtual public void Combine(GameObject target)
    {
        Debug.Log("Combine is triggering");
        //combine = Instantiate(combineHelper, transform.position, Quaternion.identity);
        combineHelper = new GameObject("CombineHelper");
        combineHelper.transform.position = target.transform.position;
        combineHelper.AddComponent<CombineHelper>();
        CombineHelper helper = combineHelper.GetComponent<CombineHelper>();
        combineHelper.transform.parent = frontgrondLayer;
        helper.SetBackChild(gameObject);
        helper.SetFrontChild(target);
        helper.SetFrontLayer(frontgrondLayer);
        helper.SetBackLayer(backgroundLayer);
    }
    virtual public void Dectect()
    {
        //Vector2 DetectCenterFall = transform.position;
        Collider2D[] list = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);

        GameObject combineTarget=null;
        foreach (Collider2D c in list)
        {
            if (c.transform.parent&& c.transform.parent.gameObject.tag == "CombineTypeTwo")
            {
                Debug.Log("target Found ");
                combineTarget = c.transform.parent.gameObject;
                break;
            }
        }
        if (combineTarget != null) {

            Combine(combineTarget);
        }
        

        Debug.Log(list.Length);
    }

}
