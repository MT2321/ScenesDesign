using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MotionController : MonoBehaviour
{
    private float m_speed = 0.1f;
    private float currentSpeed;
    void Start()
    {
        currentSpeed = m_speed;
    }

    void Update()
    {
        Vector3 cameraVector = Camera.main.transform.forward;
        float forwardMotion = Input.GetAxis("Vertical");
        cameraVector = new Vector3(cameraVector.x, 0, cameraVector.z);
        transform.position += forwardMotion * cameraVector * currentSpeed;
    }

}