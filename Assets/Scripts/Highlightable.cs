using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    EventPadController eventPadController;
    public GameObject eventPad;

    public Material highlightMat;
    Material originalMat;

    bool isHighlight = false;

    // Use this for initialization
    void Start()
    {
        eventPadController = eventPad.GetComponent<EventPadController>();
        originalMat = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void ToggleHighlightMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = isHighlight ? originalMat : highlightMat;
        isHighlight = !isHighlight;

        //Debug.Log("Toggled Highlight Material");
    }

    private void OnMouseUp()
    {
        //If I am subscribed and dropped,
        if (eventPadController && EventPadController.Contains(this))
        {
            // do something for everyone who is subscribed
            eventPadController.DoSomething();
        }
    }


}
