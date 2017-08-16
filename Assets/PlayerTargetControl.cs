using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTargetControl : MonoBehaviour {
    private int floorMask;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, 100))
            {
                if (floorHit.collider.tag == "Ground")
                {
                    transform.position = floorHit.point;
                    GameManager.Instance.player.standing = false;
                }
            }
        }
    }
}
