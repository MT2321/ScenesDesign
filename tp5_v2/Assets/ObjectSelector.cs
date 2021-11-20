using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C# example.
public class ObjectSelector : MonoBehaviour
{
    public GameObject MainCrossHair;
    public GameObject PickCrossHair;
    private Camera camera;
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    private float maxCastDistance = 1F;

    private bool HOLD = true;
    private bool RELEASE = false;
    private bool hold_release_state;

    void Start()
    {
        camera = GetComponent<Camera>();
        // hold_release_state = RELEASE;


    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = (1 << 3);

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer


        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, maxCastDistance, layerMask))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            MainCrossHair.SetActive(false);
            PickCrossHair.SetActive(true);
            HandleInput(hit.transform);

        }
        else
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            MainCrossHair.SetActive(true);
            PickCrossHair.SetActive(false);
        }
    }


    void HandleInput(Transform t)
    {
        Debug.Log("hold_release_state: " + hold_release_state);

        bool click = Input.GetKeyDown("space");
        Debug.Log(click);
        Debug.Log(t.tag);

        if (t.tag == "pepsi" && click)
        {
            if (hold_release_state == RELEASE)
            {
                hold_release_state = HOLD;
            }
            else
            {
                hold_release_state = RELEASE;
                Debug.Log("RELEASING!");
            }
        }

        if (hold_release_state == HOLD)
        {
            Debug.Log("HOLDING!");

            t.transform.position = camera.transform.position + (camera.transform.forward) * maxCastDistance * 0.6F;
        }
        if (hold_release_state == RELEASE)
        {
            Debug.Log("RELEASE!");
            // t.GetComponent<Rigidbody>().useGravity = true; 
        }

    }

}


