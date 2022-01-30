using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public LayerMask TargetLayer;
    public LayerMask BlockLayer;
    [SerializeField] private float suckforceX;
    [SerializeField] private float suckforceY;
    protected Vector2 AffectSize;
    protected Vector2 RelifSize;
    protected float AffectDistance;
    protected float RelifDistance;
    public float VelocityReduceIndex;
    public float xVelocityReduce = 1;


    protected float currdistance;
    public float affectprop;
    public float relifprop;
    public float affectwidth;
    public float relifwidth;

    public float FanDistance = 5;
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
    protected Vector2 direction;
    protected Vector2 RelifCenterDraw;
    protected Vector2 AffectCenterDraw;

    // Start is called before the first frame update
    void Start()
    {
        mycolor = sp.color;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
    }

    // Update is called once per frame
    virtual public void Update()
    {
        direction = (-detectpos.up);
        RaycastHit2D lengthcheck = Physics2D.Raycast(detectpos.position, direction, FanDistance, BlockLayer);

        if (lengthcheck)
        {

            //currdistance = //Mathf.Abs((lengthcheck.transform.position - detectpos.position).magnitude)*0.8f;
            currdistance = lengthcheck.distance;
        }
        else
        {

            currdistance = FanDistance;
        }

        AffectDistance = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * affectprop;
        RelifDistance = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * relifprop;

        AffectSize = new Vector2(affectwidth, AffectDistance);
        RelifSize = new Vector2(relifwidth, RelifDistance);

        RelifCenterDraw = (Vector2)(detectpos.transform.position) + direction * relifOffset + direction * RelifDistance / 2;
        AffectCenterDraw = RelifCenterDraw + direction * AffectDistance / 2 + direction * affectOffset + direction * RelifDistance / 2; //+ direction * AffectDistance/2

        RelifCenter = (Vector2)(detectpos.transform.position) + direction * relifOffset; //+ direction * RelifDistance / 2;



        AffectCenter = RelifCenter + direction * RelifDistance + direction * affectOffset; //+ direction * AffectDistance/2;



        //RelifCenter = new Vector2(transform.position.x, detectpos.position.y - relifOffset - RelifSize.y / 2);
        //AffectCenter = new Vector2(transform.position.x, RelifCenter.y - RelifSize.y / 2 - affectOffset - AffectSize.y / 2);

        if (active)
        {
            SuckDetect();
            PushDetect();
        }
    }

    public void activate()
    {
        active = true;
        Debug.Log("I am an activated fun");
        particleeffect.SetActive(true);
        lightindicator.SetActive(true);

    }

    public void deactivate()
    {
        active = false;
        particleeffect.SetActive(false);
        lightindicator.SetActive(false);
    }




    virtual public void SuckDetect()
    {
        //Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(AffectCenter,AffectSize, 0, TargetLayer);

        RaycastHit2D[] AttackTarget = Physics2D.BoxCastAll(AffectCenter, new Vector2(affectwidth, 0.2f), 0f, direction, AffectDistance, TargetLayer);

        if (AttackTarget.Length > 0)
        {
            foreach (RaycastHit2D target in AttackTarget)
            {
                Vector2 forcedirection = (detectpos.position - target.transform.position).normalized;

                //forcedirection = new Vector2(forcedirection.x, forcedirection.y);
                if (target.transform.gameObject.GetComponent<Rigidbody2D>())
                {
                    target.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcedirection.x * suckforceX, forcedirection.y * suckforceY), ForceMode2D.Impulse);

                }
            }
        }
    }
    virtual public void PushDetect()
    {

        RaycastHit2D[] AttackTarget = Physics2D.BoxCastAll(RelifCenter, new Vector2(relifwidth, 0.2f), 0f, direction, RelifDistance, TargetLayer);

        //Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(RelifCenter, RelifSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
            // Debug.Log("something to rise");
            foreach (RaycastHit2D target in AttackTarget)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                Rigidbody2D rb = target.transform.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x / xVelocityReduce, 0), Time.deltaTime * VelocityReduceIndex);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            if (Mathf.Abs(direction.y) > 0.2f)
            {
                //Gizmos.color = Color.blue;
                Gizmos.DrawCube(AffectCenterDraw, AffectSize);
                Gizmos.DrawCube(RelifCenterDraw, RelifSize);
            }
            else
            {

                Gizmos.DrawCube(AffectCenterDraw, new Vector2(AffectSize.y, AffectSize.x));
                Gizmos.DrawCube(RelifCenterDraw, new Vector2(RelifSize.y, RelifSize.x));

            }
        }
    }

}

