using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGrouper : MonoBehaviour {


    private Interactable parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Interactable oI = other.GetComponent<Interactable>();
        if (!oI || !oI.groupable) return;
        if (parent == null) parent = oI;
        else if (parent != null && parent != oI && oI.transform.parent != parent.transform) parent.AddToGroup(oI);
    }

    private void OnTriggerExit(Collider other)
    {
        if (parent != null && other.GetComponent<Interactable>() == parent) parent = null;
    }
}
