using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    Hashtable prefabsToBuild = new Hashtable();

    public GameObject prefab1, prefab2, prefab3, prefab4;

    //fixme: get runtime build height
    public float buildHeight = 0.6f;
    float buildY;

    KeyCode autoBuildKey = KeyCode.LeftShift;

    // Use this for initialization
    void Start()
    {
        buildY = buildHeight;

        prefabsToBuild.Add(KeyCode.Alpha1, prefab1);
        prefabsToBuild.Add(KeyCode.Alpha2, prefab2);
        prefabsToBuild.Add(KeyCode.Alpha3, prefab3);
        prefabsToBuild.Add(KeyCode.Alpha4, prefab4);

    }

    // Update is called once per frame
    void Update()
    {

        //Input.inputString contains a string of all the keys entered during this frame, might be better
        if (Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(autoBuildKey)) ||
            Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetKey(KeyCode.Alpha2) && Input.GetKey(autoBuildKey)) ||
            Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetKey(KeyCode.Alpha3) && Input.GetKey(autoBuildKey)) ||
            Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetKey(KeyCode.Alpha4) && Input.GetKey(autoBuildKey)))
        {
            foreach (KeyCode k in prefabsToBuild.Keys)
            {
                //Debug.Log("Checking " + k);
                if (Input.GetKeyDown(k) || (Input.GetKey(k) && Input.GetKey(autoBuildKey)))
                {
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                    curPosition.y = buildY;
                    Instantiate((GameObject)prefabsToBuild[k], curPosition, transform.rotation);

                    //Debug.Log("Built at " + curPosition);
                    break;
                }
            }
        }

    }
}
