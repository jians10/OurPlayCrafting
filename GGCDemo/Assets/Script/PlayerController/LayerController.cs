using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("background Interact")]
    public LayerMask player;
    public LayerMask playerinMask;
    public bool inMask;
    //public SpriteRenderer sp;
    //private Color mycolor;
    public Animator myanimator;
    void Start()
    {
        myanimator = GetComponent<Animator>();
        //mycolor = sp.color;
        //sp.color = new Color32(200, 150, 150, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getInMask()
    {

        if (DetectCollision())
        {
            gameObject.layer = LayerMask.NameToLayer("playerInMask");
            inMask = true;
            MaskColor();
        }

    }
    public void getOutMask()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
        inMask = false;
        NormalColor();
    }
    private void MaskColor()
    {
        Debug.Log("CHANGE COLOR");
        //sp.color = new Color32(200, 150, 150, 255);
        myanimator.Play("InMask");
    }
    private void NormalColor()
    {
        myanimator.Play("Normal");
        //sp.color = mycolor;
    }
    private bool DetectCollision()
    {
        Collider2D[] list = Physics2D.OverlapBoxAll(transform.position, new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, transform.localScale.z - 0.1f), 0);
        foreach (Collider2D c in list)
        {
            if (c.transform.gameObject.layer == LayerMask.NameToLayer("hiddenground"))
            {
                return false;
            }
        }
        return true;
    }
}
