using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public LayerMask TargetLayer;
    public float suckforce;
    public float pushforce;
    public Vector2 DetectSize;
    public Vector2 PushSize;
    public Vector2 pushOffset;
    public Vector2 DetectCenter;
    public Vector2 PushCenter;
    public Vector2 detectOffset;
    public SpriteRenderer sp;
    private Color mycolor;
    public float alpha;

    // Start is called before the first frame update
    void Start()
    {
        mycolor = sp.color;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
        DetectCenter = new Vector2(transform.position.x + detectOffset.x, transform.position.y + detectOffset.y - DetectSize.y/2);
        PushCenter = new Vector2(transform.position.x + pushOffset.x, transform.position.y + pushOffset.y - PushSize.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
       SuckDetect();
       PushDetect();
    }

    void SuckDetect()
    {
        Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(DetectCenter, DetectSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
           // Debug.Log("something to rise");
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
    void PushDetect() {

        Collider2D[] AttackTarget = Physics2D.OverlapBoxAll(PushCenter, PushSize, 0, TargetLayer);
        if (AttackTarget.Length > 0)
        {
           // Debug.Log("something to rise");
            foreach (Collider2D target in AttackTarget)
            {
                Vector2 direction = (transform.position - target.transform.position).normalized;
                Rigidbody2D rb = target.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    //target.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * pushforce, ForceMode2D.Impulse);
                   // Debug.Log("stop in there");
                    rb.velocity =Vector2.Lerp(rb.velocity, new Vector2(rb.velocity.x,0), Time.deltaTime*5);
                    //target.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * pushforce, ForceMode2D.Impulse);
                }
            }
        }
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawCube(DetectCenter, DetectSize);
        //Gizmos.DrawCube(PushCenter, PushSize);
    }

}

