using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNeo : MonoBehaviour
{
    public Transform[] background;
    private float[] parallaxScales; // proportion of the camera's movement to move th background
    // Start is called before the first frame update
    public float smoothing = 0;
    public float degree;
    private Transform cam;
    private Vector3 previousCamPos; // the position of camera in the previous fram

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;
        parallaxScales = new float[background.Length];
        for (int i = 0; i < background.Length; i++) {
            parallaxScales[i] = background[i].position.z * -degree;
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < background.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = background[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, background[i].position.y, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            
        }
        previousCamPos = cam.position;
    }
}
