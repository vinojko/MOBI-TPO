using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public static RotateCamera instance;

    private Touch initTouch = new Touch();
    public Camera cam;
    public Camera referenceCam;
    private float CamMoveSpeed = 2f;

    private float rotX = 0f;        
    private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 0.5f;
    public float dir = -1;

    //Za kamero
    private bool lookRight, lookLeft = false;

    void Awake()
    {

    }

    void Start(){
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;

    }
    void FixedUpdate()
    {
        foreach(Touch touch in Input.touches){
            if (!didLook())
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initTouch = touch;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = initTouch.position.x - touch.position.x;
                    float deltaY = initTouch.position.y - touch.position.y;

                    rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                    rotY += deltaX * Time.deltaTime * rotSpeed * dir;

                    Mathf.Clamp(rotX, -45f, 45f);
                    Mathf.Clamp(rotY, -45f, 45f);



                    cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, rotY, 0f);

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }
            }

        }

        if (didLook()){

            cam.transform.position = Vector3.Lerp(cam.transform.position, referenceCam.transform.position, CamMoveSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, referenceCam.transform.rotation, CamMoveSpeed * Time.deltaTime);
            GameManager.instance.UpdateGameState(GameState.Odzivnost);
        }
    }

    bool didLook(){
        float angle = cam.transform.eulerAngles.y;


        if (angle >= 140f)lookRight = true;
        else if (angle <= 90f)lookLeft = true;

        return lookLeft && lookRight;

    }
}
