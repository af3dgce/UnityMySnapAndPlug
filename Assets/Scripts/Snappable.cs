using SnapAndPlug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PointerSnapMode
{
    ABSOLUTE_PROJECTION,
    RELATIVE_TO_LAST_SNAP
}

public class Snappable : Interactable
{
    private SnapPiece _currentPiece;

    private PointerSnapMode _snapMode;


    public float initialDistanceToSpawnAt = 100f;
    private float _currentDistanceToPositionAt;


    private SnapGhostable snapGhostable;

    new protected void Start()
    {
        snapGhostable = gameObject.GetComponent<SnapGhostable>();
    }




    new protected void OnMouseDown()
    {
        base.OnMouseDown();
        if (snapGhostable) snapGhostable.MakeGhost();
    }

    new protected void OnMouseUp()
    {
        if (snapGhostable) snapGhostable.UnGhost();
        base.OnMouseUp();
    }



}


