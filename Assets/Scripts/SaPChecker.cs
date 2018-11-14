using SnapAndPlug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaPChecker : MonoBehaviour
{
    SnapPiece snapPiece;

    // Use this for initialization
    void Start()
    {
        snapPiece = GetComponent<SnapPiece>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Vector3 mousePositionOnDown = Input.mousePosition;

            //Get a ray from the camera to the mouse pointer
            Ray ray = Camera.main.ScreenPointToRay(mousePositionOnDown);
            RaycastHit hit;

            //check if it collides 
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("collided");
                //Debug.DrawRay(mousePositionOnDown, hit.transform.position, Color.white);

            }

        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) return; //terrain

        //Debug.Log("On collision, got " + snapPiece.CountUnconnectedSockets() + " unconnected Sockets");

        Ray ray = new Ray(transform.position, other.transform.position);
        Debug.DrawLine(transform.position, other.transform.position);
        List<RaycastHit> hits;
        SnapPiece first = snapPiece.FirstSnapPieceAlongRayWithAvailableSockets(ray, out hits);

        if (hits.Count > 0)
        {
            SnapSocket eSocket = first.GetFirstSocketNamed("E Socket");

            SnapSocket wSocket = snapPiece.GetFirstSocketNamed("W Socket");

            eSocket.SnapTo(wSocket);



            /*
            Debug.Log(gameObject.name + " hit something");
            Debug.Log(first + " was the first snap piece along the collision ray");
            Debug.Log(hits.Count + " hits total");
            foreach(RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name + " was hit");
            }
            */

        }

        //snapPiece.GetAllUnconnectedSockets();
    }
}
