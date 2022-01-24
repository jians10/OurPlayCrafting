using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float fallspeed= 5f;
    //private float step;
    public float risespeed = 5f;
    private Rigidbody2D rb;
    public float detectheight;
    public float detectwide;
    Vector2 DetectCenterFall;
    Vector2 DetectCenterRise;
    public float DetectOffset=0.1f;
    private int LayerMask;
    void Start()
    {
        //LayerMask=  ~LayerMask.GetMask("Enemy");
        //step = fallspeed * Time.deltaTime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectCenterFall = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2 - detectheight / 2-DetectOffset);
        //currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, Time.deltaTime);
        DetectCenterRise = new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2 + detectheight / 2 +DetectOffset);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(DetectCenterFall,new Vector3(detectwide,detectheight,1));
        Gizmos.DrawCube(DetectCenterRise, new Vector3(detectwide, detectheight, 1));
    }


    IEnumerator FallingwithVelocity()
    {
        float i = 0.01f;
        while (fallspeed > i)
        {
            //Physics2D.BoxCast(, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance = Mathf.Infinity);
            if (Physics2D.OverlapBox(DetectCenterFall, new Vector2(detectwide, detectheight), 0)){
                Debug.Log("no way to go down");
                break;
            }
            rb.velocity = new Vector2(rb.velocity.x, -fallspeed / i); // !! For X axis positive force
            i = i + Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
        yield return null;
    }

    IEnumerator RisingwithVelocity()
    {
        float i = 0.01f;
        while (fallspeed > i)
        {
            //Physics2D.BoxCast(, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance = Mathf.Infinity);
            if (Physics2D.OverlapBox(DetectCenterRise, new Vector2(detectwide, detectheight), 0))
            {
                Debug.Log("no way to go up");
                break;
            }
            rb.velocity = new Vector2(rb.velocity.x, risespeed / i); // !! For X axis positive force
            i = i + Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
        yield return null;
    }





    public void FallValue()
    {
        Debug.Log("I will fall later");
        Vector2 currpos = transform.position;
        StartCoroutine(FallingwithVelocity());
    }

    public void RiseValue()
    {
        Debug.Log("I will rise later");
        Vector2 currpos = transform.position;
        StartCoroutine(RisingwithVelocity());
    }



    //-------------------------------------- not used-----------------------------------------//


}
