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

        RaycastHit2D lengthcheck = Physics2D.Raycast(detectpos.position, detectpos.up, AffectDistance, BlockLayer);

        if (lengthcheck)
        {

            currdistance = Mathf.Abs(lengthcheck.transform.position.y - detectpos.position.y);
        }
        else
        {

            currdistance = AffectDistance;
        }
        float AffectSizey = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * affectprop;
        float RelifSizey = (currdistance - affectOffset - relifOffset) / (affectprop + relifprop) * relifprop;

        AffectSize = new Vector2(width, AffectSizey);
        RelifSize = new Vector2(width, RelifSizey);


        AffectCenter = new Vector2(transform.position.x, detectpos.position.y + affectOffset + AffectSize.y / 2);
        RelifCenter = new Vector2(transform.position.x, AffectCenter.y + RelifSize.y / 2 + relifOffset + AffectSize.y / 2);

        if (active)
        {
            SuckDetect();
            PushDetect();
        }
    }



    override public void SuckDetect()
    {
        //Debug.Log("Detected Enemey");
        Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(AffectCenter, AffectSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
            // Debug.Log("something to rise");
            foreach (Collider2D target in AttackTarget)
            {
                //Vector2 direction = (transform.position - target.transform.position).normalized;
                if (target.gameObject.GetComponent<Rigidbody2D>())
                {
                    target.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * pushforce, ForceMode2D.Impulse);

                }
            }
        }
    }
    override public void PushDetect()
    {

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
                    rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x, 0), Time.deltaTime * 5);
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

