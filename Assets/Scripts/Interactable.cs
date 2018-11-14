using SnapAndPlug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropMethod
{
    Bottom,
    Origin,
    Center,
    None
}

public class Interactable : MonoBehaviour
{

    protected Vector3 screenPoint;
    protected Vector3 offset;

    protected bool pickedUp;

    public bool groupable = true;

    protected Vector3 initialPos;

    //the max distance from the object's spawn point before it's subject to destruction
    public float killDist = 100;
    protected float checkSelfDestructSeconds = 5f;

    public DropMethod dropMethod = DropMethod.None;

    //protected List<Interactable> interactableChildren;

    //protected Dictionary<Interactable, Rigidbody> interactableChildren;
    public List<Interactable> intitialChildren;





    // add an Interactable to this Interactable, as a group
    public void AddToGroup(Interactable iOtoAdd)
    {
        GetComponent<Rigidbody>().mass += iOtoAdd.GetComponent<Rigidbody>().mass;
        Destroy(iOtoAdd.GetComponent<Rigidbody>());
        iOtoAdd.gameObject.transform.SetParent(gameObject.transform);

        this.GetComponent<Renderer>().bounds.Encapsulate(iOtoAdd.GetComponent<Renderer>().bounds);
    }

    //remove incoming from this group or dissolve this Interactable as group
    void RemoveFromGroup(Interactable iOtoRemove = null, bool removeAll = false)
    {

    }


    // Use this for initialization
    protected void Start()
    {
        initialPos = transform.position;
        if (killDist > 0) InvokeRepeating("CheckSelfDestruct", checkSelfDestructSeconds, checkSelfDestructSeconds);







        //Bounds b = new Bounds(this.transform.position, Vector3.zero);

        foreach (Interactable child in intitialChildren)
        {
            AddToGroup(child);
        }
        












    }

    // Update is called once per frame
    void Update()
    {
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        //remove my own rigidboby and other's rigidbody and create a new Interactable to group these two together, add its own rigidbody
        Interactable iO = other.GetComponent<Interactable>();
        if (iO == null) return;

        
        Destroy(this.GetComponent<Rigidbody>());
        Destroy(iO.GetComponent<Rigidbody>());

        group = Resources.Load<Interactable>("InteractableGroup");
        group.interactableChildren.Add(this.GetComponent<Interactable>());
        group.interactableChildren.Add(other.GetComponent<Interactable>());

    }
    */



    //Must have a collider (non-trigger)
    protected void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        //lift it up a smidge
        //offset.y += 0.05f;
        //GetComponent<SnapGhostable>().MakeGhost();

        //gameObject.GetComponent<MySnappableSpawner>().SpawnGhostToMouse(screenPoint, offset);


        PickedUp = true;



    }

    // Called every frame when the user has clicked on a GUIElement or Collider and is still holding down the mouse.

    protected void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }




    protected void OnMouseUp()
    {

        //move it up a smidge(?)
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Debug.Log("About to unghost at " + transform.position);
        //GetComponent<SnapGhostable>().UnGhost();
        //Drop();
        PickedUp = false;

        DropSelf(dropMethod);

    }



    public bool PickedUp
    {
        get
        {
            return pickedUp;
        }

        set
        {
            //Debug.Log(value ? "Picked Up" : "Dropped");
            pickedUp = value;
        }
    }

    private void CheckSelfDestruct()
    {
        if (Vector3.Distance(transform.position, initialPos) > killDist)
        {
            //Debug.Log("Goodbye, cruel world");
            Destroy(gameObject);
        }
    }


    //helps drop object on a supporting surface
    //especially useful if the object is ghosted when carried, so it doesn't drop through the floor
    void DropSelf(DropMethod method)
    {
        if (dropMethod == DropMethod.None) return;

        Bounds bounds = new Bounds();
        if (gameObject.GetComponent<Renderer>()) bounds = gameObject.GetComponent<Renderer>().bounds;

        RaycastHit hit;
        float yOffset = 0.0f;
        int savedLayer = gameObject.layer;
        gameObject.layer = 2; //ignore self-raycast

        // see if this ray hit something
        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit))
        {
            // determine how the y will need to be adjusted
            switch (method)
            {
                case DropMethod.Bottom:
                    yOffset = gameObject.transform.position.y - bounds.min.y;
                    break;
                case DropMethod.Origin:
                    yOffset = 0.0f;
                    break;
                case DropMethod.Center:
                    yOffset = bounds.center.y - gameObject.transform.position.y;
                    break;
            }
            gameObject.transform.position = hit.point;
            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);


        }
        // restore layer
        gameObject.layer = savedLayer;

        // from https://forum.unity.com/threads/here-is-an-editor-script-to-help-place-objects-on-ground.38186/
    }




}