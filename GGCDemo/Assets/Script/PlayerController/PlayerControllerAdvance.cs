using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAdvance : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    public int extraJumps;
    public int extraJumpVal = 2;

    [Header("Components")]
    private Rigidbody2D rb;
    //public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;
   
    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public PlatformMovement currPlatform=null;


    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;


    
    [Header("ParticleSystem")]
    public ParticleSystem dust;


    void Start()
    {
        extraJumps = extraJumpVal;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        bool wasOnGround = onGround;
        RaycastHit2D hitRight;
        RaycastHit2D hitLeft;
        //onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        hitLeft = Physics2D.Raycast(transform.position + colliderOffset,Vector2.down, groundLength,groundLayer);
        hitRight = Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        //if (hitLeft)
        //{
        //    if (hitLeft.transform.gameObject.GetComponent<PlatformMovement>())
        //    {
        //        currPlatform = hitLeft.transform.gameObject.GetComponent<PlatformMovement>();
        //    }
        //}
        //else if (hitRight)
        //{
        //    if (hitRight.transform.gameObject.GetComponent<PlatformMovement>())
        //    {
        //        currPlatform = hitRight.transform.gameObject.GetComponent<PlatformMovement>();
        //    }
        //}
        //else { 
        //    currPlatform==null
        //}


        onGround = hitLeft || hitRight;
        
        if (onGround) {
            extraJumps = extraJumpVal;
        }

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            // do not miss the tick, improve user experience 
            jumpTimer = Time.time + jumpDelay;
        }
        //animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate()
    {
        moveCharacter(direction.x);
        
        if (jumpTimer > Time.time && extraJumps>0) {
            extraJumps--;
            //if(onGround&&)
            Jump();
        }
        else if (jumpTimer > Time.time && onGround && extraJumps == 0)
        {
            Jump();
        }

        modifyPhysics();
    }
    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        //animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("vertical", rb.velocity.y);
    }
    void Jump()
    {
        if (currPlatform) {
            currPlatform.FallValue();
        }
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
        CreateDust();
    }
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            // if we jump but not holding the jump button, we can jump higher
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        //transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        //facingRight = !facingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
        CreateDust();
    }
    // jump sequence without the support of animation 
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
    void CreateDust()
    {
        dust.Play();
        //Debug.Log("play dust");

    }
}