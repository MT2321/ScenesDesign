using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// C# example.
public class ObjectSelector : MonoBehaviour
{
    public GameObject MainCrossHair;
    public GameObject ActionCrossHair;
    public GameObject TVCrossHair;
    public Material tvscreen;
    public GameObject WhiteNoise;
    private Camera camera;
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    private float maxCastDistance = 1F;

    private bool HOLD = true;
    private bool RELEASE = false;
    private bool ON = true;
    private bool OFF = false;
    private bool on_off_state;
    private bool hold_release_state;


    void Start()
    {
        camera = GetComponent<Camera>();
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

            TVCrossHair.SetActive(false);
            ActionCrossHair.SetActive(false);
            MainCrossHair.SetActive(false);
            if (hit.transform.tag == "tv")
                TVCrossHair.SetActive(true);
            else if (hit.transform.tag == "pepsi")
                ActionCrossHair.SetActive(true);
            else
                MainCrossHair.SetActive(true);

            HandleInput(hit.transform);

        }
        else
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");

            MainCrossHair.SetActive(true);
            ActionCrossHair.SetActive(false);
            TVCrossHair.SetActive(false);
        }
    }


    void HandleInput(Transform t)
    {
        Debug.Log("hold_release_state: " + hold_release_state);

        bool click = Input.GetKeyDown("g");
        Debug.Log(click);
        Debug.Log(t.tag);

        if (t.tag == "pepsi" && click)
        {
            if (hold_release_state == RELEASE)
            {
                hold_release_state = HOLD;
                t.GetComponent<Rigidbody>().useGravity = false;
                t.GetComponent<Rigidbody>().drag = 10;
            }
            else
            {
                hold_release_state = RELEASE;
                Debug.Log("RELEASING!");
                t.GetComponent<Rigidbody>().useGravity = true;

            }
        }
        else if (t.tag == "tv" && click)
        {
            Debug.Log("Hitting tv");
            // TextMesh newText = ActionCrossHair.transform.GetComponentInChildren<TextMesh>();
            // Debug.Log(newText.text);
            MainCrossHair.SetActive(false);
            ActionCrossHair.SetActive(false);
            TVCrossHair.SetActive(true);
            // tvscreen.EnableKeyword("_EMISSION");
            // Color on_color = new Color(71, 190, 191);
            // tvscreen.SetColor("_EmissionColor", on_color);
            if (on_off_state == OFF)
            {
                on_off_state = ON;
                tvscreen.EnableKeyword("_EMISSION");
                WhiteNoise.SetActive(true);

            }
            else if (on_off_state == ON)
            {
                on_off_state = OFF;
                Debug.Log("Turning OFF!");
                // tvscreen.SetColor("_EmissionColor", Color.black);
                tvscreen.DisableKeyword("_EMISSION");
                WhiteNoise.SetActive(false);

            }
            // List<Material> myMaterials;
            // t.gameObject.GetComponent<Renderer>().GetMaterials(myMaterials);
            // foreach (var item in myMaterials)
            // {
            //     Debug.Log(item.name);
            // }

            // GameObject newText = ActionCrossHair.GetChild(0);
            // Debug.Log(newText.GetComponents<UnityEngine.UI.Text>);
            // newText = "[A] Turn off tv";
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


