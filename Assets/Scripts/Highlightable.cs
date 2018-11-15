using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{

    public Material highlightMat;
    Material originalMat;

    bool isHighlight = false;

    // Use this for initialization
    void Start()
    {
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
        if (HighlightController.Contains(this))
        {
            // do something for everyone who is subscribed
            HighlightController.DoSomething();
        }
    }


}
