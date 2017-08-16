using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float PanSpeed = 30;
    public float PanBorderThickness = 10f;
    public float ScrollSpeed = 3f;
    public float MinY = 10;
    public float MaxY = 80;

    private bool canMove = true;

	// Update is called once per frame
	void Update ()
    {
        if (!canMove)
            return;
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - PanBorderThickness)
        {
            transform.Translate(Vector3.forward * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= PanBorderThickness)
        {
            transform.Translate(Vector3.back * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= PanBorderThickness)
        {
            transform.Translate(Vector3.left * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            transform.Translate(Vector3.right * PanSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * ScrollSpeed * Time.deltaTime * 1000;
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        transform.position = pos;
    }
}
