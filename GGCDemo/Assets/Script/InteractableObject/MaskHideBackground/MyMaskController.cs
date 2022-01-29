using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMaskController: MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sp;
    public float deactivealpha=1f;
    public float activealpha=0.1f;
    //public LayerMask Player;
    //public LayerMask PlayerInMask;
    //public LayerMask hiddenground;
    public bool activated=false;
    public float activateTime = 1f;
    public float deactivateTime = 1f;
    public float originalscale = 0.25f;
    public float activatescale = 3f;
    private Collider2D mycollider;
    //public LayerMask hidden;
    void Start()
    {
        mycollider = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        hideDetector();  
    }

    void hideDetector() {
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, deactivealpha);
        mycollider.enabled = false;
    }
    IEnumerator graduallyscale(float originalscale, float activatescale, float time, float originalalpha, float activealpha, bool active) {

        float currentTime = 0.0f;

        do
        {
            currentTime += Time.deltaTime;
            originalalpha = Mathf.Lerp(originalalpha, activealpha, currentTime / time);
            originalscale = originalscale + (activatescale - originalscale) * currentTime / time;
            gameObject.transform.localScale = new Vector3(originalscale, originalscale, 1);
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, originalalpha);

            yield return new WaitForEndOfFrame();
        } while (time >= currentTime);
    }

    //IEnumerator ScaleOverTime(float time)
    //{
    //    Vector3 originalScale = transform.localScale;
    //    Vector3 destinationScale = new Vector3(2.0f, 2.0f, 2.0f);

    //    float currentTime = 0.0f;

    //    do
    //    {
    //        transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
    //        currentTime += Time.deltaTime;
    //        yield return null;
    //    } while (currentTime <= time);

    //    Destroy(gameObject);
    //}


    void deactive() {
        activated = false;
        mycollider.enabled = false;
        StartCoroutine(graduallyscale(activatescale, originalscale, 2,activealpha,deactivealpha, false));
        
    }


    void active() {
        activated = true;
        mycollider.enabled = true;
        StartCoroutine(graduallyscale(originalscale, activatescale,2,deactivealpha, activealpha,true));
        //activated = true;
        //var newScale : float = Mathf.Lerp(0.5, 3, Time.deltaTime / 10);
        //transform.localScale = Vector3(newScale, newScale, 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C)){
            if (!activated)
            {
                active();
            }
            else {
                deactive();
            }
        }
        Vector2 mousePositionOnScreen = Input.mousePosition;
        //mousePositionOnScreen.z = screenPosition.z;
        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        transform.position = Vector2.Lerp(transform.position, mousePositionInWorld, Time.deltaTime*10);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LayerController>())
        {
            collision.gameObject.GetComponent<LayerController>().getInMask();
            //Debug.Log("Get a Player");

        }
        SecondLayerObject so = collision.gameObject.GetComponent<SecondLayerObject>();
        if (so)
        {
            //Debug.Log("gonna be activated");
            so.active();

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LayerController>())
        {
            collision.gameObject.GetComponent<LayerController>().getOutMask();
            //Debug.Log("Player get out");
        }
        SecondLayerObject so = collision.gameObject.GetComponent<SecondLayerObject>();
        if (so)
        {
            //Debug.Log("gonna be activated");
            so.deactive();

        }
    }
}
