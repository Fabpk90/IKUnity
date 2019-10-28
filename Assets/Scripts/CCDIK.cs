using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDIK : MonoBehaviour
{
    public List<CCDIKJoint> joints;
    public Transform goal;
    public Transform effector;

    public float distanceError = 0.01f;
    [Range(0.1f, 1f)]
    public float weight;
    public int maxIterationCount;

    private void OnEnable()
    {
        joints = new List<CCDIKJoint>();
        
        Transform joint = effector.transform.parent;

        while (joint != null && joint.transform != goal)
        {
            CCDIKJoint ikJoint = joint.GetComponent<CCDIKJoint>();

            if (ikJoint)
            {
                joints.Add(ikJoint);
            }
                

            joint = joint.transform.parent;
        }
    }
    
    //Credit to Sam Hocevar of LolEngine
    //lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts
    public static Vector3 Perpendicular(Vector3 vec) {
        return Mathf.Abs(vec.x) > Mathf.Abs(vec.z) ? new Vector3(-vec.y, vec.x, 0f)
            : new Vector3(0f, -vec.z, vec.y);
    }
    
    void SolveIK()
    {
        Vector3 goalPosition = Vector3.Lerp(effector.transform.position, goal.position, weight);
        float sqrDistance;

        int iterationCount = 0;

        do
        {
            //we skip the effector (base bone)
            for (int j = 0; j < joints.Count; j++)
            {
                for (int i = 0; i < j + 3 && i < joints.Count; i++)
                {
                    RotateBone(joints[j], goalPosition);
                }
            }

            sqrDistance = (goalPosition - effector.transform.position).sqrMagnitude;
            
            iterationCount++;
        } while (iterationCount < maxIterationCount && sqrDistance > distanceError);
    }
    

    private void RotateBone(CCDIKJoint currentJoint, Vector3 goalPosition)
    {
        var jointPosition = currentJoint.transform.position;

        Vector3 toEffector = effector.transform.position - jointPosition;
        Vector3 toGoal = goalPosition - jointPosition;
        
        Quaternion fromToRotation = Quaternion.FromToRotation(toEffector, toGoal);
        Quaternion rotation = fromToRotation * currentJoint.transform.rotation;

        Vector3 angles = rotation.eulerAngles;

        if (currentJoint.isClampingX)
            angles.x = Mathf.Clamp(angles.x, currentJoint.minAngleX, currentJoint.maxAngleX);
        if (currentJoint.isClampingY)
            angles.y = Mathf.Clamp(angles.y, currentJoint.minAngleY, currentJoint.maxAngleY);
        if (currentJoint.isClampingZ)
            angles.z = Mathf.Clamp(angles.z, currentJoint.minAngleZ, currentJoint.maxAngleZ);

        rotation.eulerAngles = angles;
        
        currentJoint.transform.rotation = rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SolveIK();
    }
}
