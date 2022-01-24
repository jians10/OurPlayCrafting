using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpforce;
    private float moveInput;
    private Rigidbody2D rb;
    public bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    private int extraJumps;
    public int extraJumpVal=2;
    public ParticleSystem dust;


    void Start()
    {
        extraJumps = extraJumpVal;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatisGround);
        if (isGrounded) {
            extraJumps=extraJumpVal;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
            CreateDust();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true) {
            rb.velocity = Vector2.up * jumpforce;
            CreateDust();
        }

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if ((!facingRight&& moveInput > 0) ||(facingRight && moveInput<0)) {
            Flip();
        }
    }
    void Flip() {
        CreateDust();
        facingRight = !facingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }
    void CreateDust() {
        dust.Play();
        Debug.Log("play dust");
    
    }
}
