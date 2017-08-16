using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X,
    Y,
    Z
}

public class Rotator : MonoBehaviour {

    public Axis rotationAxis;
    public float rotationSpeed;
    public bool canRotate;

    private void Update()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(rotationAxis == Axis.X ? rotationSpeed * Time.deltaTime : 0, rotationAxis == Axis.Y ? rotationSpeed * Time.deltaTime : 0, rotationAxis == Axis.Z ? rotationSpeed * Time.deltaTime : 0));
        }
    }

}
