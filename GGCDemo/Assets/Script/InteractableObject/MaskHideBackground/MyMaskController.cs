using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMaskController: MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sp;
    public float alpha;
    public LayerMask Player;
    public LayerMask PlayerInMask;
    public LayerMask hiddenground;
    //public LayerMask hidden;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        showDetector();  
    }

    void showDetector() {
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("I am here");
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePositionOnScreen = Input.mousePosition;
        //mousePositionOnScreen.z = screenPosition.z;
        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        transform.position = Vector2.Lerp(transform.position, mousePositionInWorld, Time.deltaTime*10);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControllerAdvance>()) {
            collision.gameObject.GetComponent<PlayerControllerAdvance>().getInMask();
            Debug.Log("Get a Player");

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControllerAdvance>()) {
            collision.gameObject.GetComponent<PlayerControllerAdvance>().getOutMask();
            Debug.Log("Player get out");
        }
    }
}
