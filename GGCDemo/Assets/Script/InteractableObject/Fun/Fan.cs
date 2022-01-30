using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public LayerMask TargetLayer;
    public LayerMask BlockLayer;
    public float suckforce;
    protected Vector2 AffectSize;
    protected Vector2 RelifSize;
    protected float currdistance;
    public float affectprop;
    public float relifprop;
    public float width;
    public float AffectDistance=5;
    public Transform detectpos;

    public float relifOffset;
    protected Vector2 AffectCenter;
    protected Vector2 RelifCenter;
    public float affectOffset;
    public GameObject particleeffect;
    public GameObject lightindicator;
    public SpriteRenderer sp;
    protected Color mycolor;
    public float alpha;
    public bool showGizmo;
    //public bool suck;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        mycolor = sp.color;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
    }

    // Update is called once per frame
    virtual public void Update()
    {
        RaycastHit2D lengthcheck = Physics2D.Raycast(detectpos.position, -detectpos.up, AffectDistance, BlockLayer);

        if (lengthcheck)
        {

            currdistance = Mathf.Abs(lengthcheck.transform.position.y - detectpos.position.y);
        }
        else {

            currdistance = AffectDistance;
        }
        float AffectSizey = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * affectprop;
        float RelifSizey = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * relifprop;

        AffectSize = new Vector2(width, AffectSizey);
        RelifSize = new Vector2(width, RelifSizey);


        RelifCenter = new Vector2(transform.position.x, detectpos.position.y - relifOffset - RelifSize.y / 2);
        AffectCenter = new Vector2(transform.position.x, RelifCenter.y - RelifSize.y / 2 - affectOffset - AffectSize.y / 2);

        if (active)
        {
            SuckDetect();
            PushDetect();
        }
    }

    public void activate() {
        active = true;
        Debug.Log("I am an activated fun");
        particleeffect.SetActive(true);
        lightindicator.SetActive(true);
    
    }

    public void deactivate() {
        active = false;
        particleeffect.SetActive(false);
        lightindicator.SetActive(false);
    }

    


    virtual public void SuckDetect()
    {
        Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(AffectCenter,AffectSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
            foreach (Collider2D target in AttackTarget)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                if (target.gameObject.GetComponent<Rigidbody2D>())
                {
                    target.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * suckforce, ForceMode2D.Impulse);

                }
            }
        }
    }
    virtual public  void PushDetect() {

        Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(RelifCenter, RelifSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
           // Debug.Log("something to rise");
            foreach (Collider2D target in AttackTarget)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                Rigidbody2D rb = target.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.velocity =Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x,0), Time.deltaTime*5);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            //Gizmos.color = Color.blue;
            Gizmos.DrawCube(AffectCenter, AffectSize);
            Gizmos.DrawCube(RelifCenter, RelifSize);
        }
    }

}

