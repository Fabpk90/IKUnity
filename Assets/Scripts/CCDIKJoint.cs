using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDIKJoint : MonoBehaviour
{
    public bool isClampingX;
    public float minAngleX;
    public float maxAngleX;

    public bool isClampingY;
    public float minAngleY;
    public float maxAngleY;

    public bool isClampingZ;
    public float minAngleZ;
    public float maxAngleZ;

    public Vector3 axis = Vector3.one;
}
