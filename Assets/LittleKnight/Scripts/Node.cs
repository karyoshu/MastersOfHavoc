using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
    public Vector3 positionOffset;

    private Color defaultcolor;
    private Renderer rend;

    public GameObject turret;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        defaultcolor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        //if (BuildManager.Instance.GetTurretToBuild() == null)
        //    return;
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = defaultcolor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        //if (BuildManager.Instance.GetTurretToBuild() == null)
        //    return;
        //if (turret != null)
        //{
        //    Debug.Log("Can't build there!");
        //    return;
        //}
        //GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();

        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        //turret.transform.SetParent(transform);

        BuildManager.Instance.OnNodeSelection(this);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
