using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunPush : Fan
{
    public float pushforce;
    //private Vector2 DetectCenter;
    //private Vector2 PushCenter;
    
    // Start is called before the first frame update

    // Update is called once per frame
    override public void Update()
    {
        
        direction = (detectpos.up);
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

        AffectCenter = (Vector2)(detectpos.transform.position) + direction * affectOffset;// + direction * AffectDistance / 2;
        RelifCenter = AffectCenter + direction *AffectDistance +  relifOffset*direction; //direction * RelifDistance / 2


        AffectCenterDraw = (Vector2)(detectpos.transform.position) + direction * affectOffset + direction * AffectDistance / 2;
        RelifCenterDraw = AffectCenterDraw + direction * relifOffset + direction * RelifDistance / 2+direction*AffectDistance/2;


        if (active)
        {
            SuckDetect();
            PushDetect();
        }
    }



    override public void SuckDetect()
    {
        //Debug.Log("Detected Enemey");
        RaycastHit2D[] AttackTarget = Physics2D.BoxCastAll(AffectCenter, new Vector2(affectwidth, 0.2f), 0f, direction, AffectDistance, TargetLayer);
        if (AttackTarget.Length > 0)
        {
            // Debug.Log("something to rise");
            foreach (RaycastHit2D target in AttackTarget)
            {
                //Vector2 direction = (transform.position - target.transform.position).normalized;
                if (target.transform.gameObject.GetComponent<Rigidbody2D>())
                {
                    target.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * pushforce, ForceMode2D.Impulse);

                }
            }
        }
    }
    override public void PushDetect()
    {

        RaycastHit2D[] AttackTarget = Physics2D.BoxCastAll(RelifCenter, new Vector2(relifwidth, 0.2f), 0f, direction, RelifDistance, TargetLayer);
        if (AttackTarget.Length > 0)
        {
            // Debug.Log("something to rise");
            foreach (RaycastHit2D target in AttackTarget)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                Rigidbody2D rb = target.transform.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, 0), Time.deltaTime * VelocityReduceIndex);
                }
            }
        }
    }



    private void OnDrawGizmos()
    {
       if (showGizmo)
       {
           if (Mathf.Abs(detectpos.up.y)>0.2f)
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

