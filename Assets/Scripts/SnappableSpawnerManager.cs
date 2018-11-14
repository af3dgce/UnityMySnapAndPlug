using SnapAndPlug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableSpawnerManager : MonoBehaviour
{
    KeyCode autoBuildKey = KeyCode.LeftShift;
    
    //fixme: get runtime build height
    public float buildHeight = 0.6f;
    float buildY;

    // Use this for initialization
    void Start()
    {
        buildY = buildHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(autoBuildKey))) {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition.y = buildY;

            GetComponent<SnappableSpawner>().SpawnGhostToMouse();

            //Instantiate((GameObject)prefabsToBuild[k], curPosition, transform.rotation);
        }


    }
}
