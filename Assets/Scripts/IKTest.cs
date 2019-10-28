using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class IKTest : UnityEngine.MonoBehaviour
{

   [System.Serializable]
   public class Point
   {
      public GameObject joint;
      public float length;

      public bool constrainX;
      public float maxX;
      public float minX;
      
      public bool constrainY;
      public float maxY;
      public float minY;
      
      public bool constrainZ;
      public float maxZ;
      public float minZ;
   }

   public List<Point> chain;
   private List<Vector3> jointPoints;
   public Transform goalPoint;
   public float marginOfError;

   private float totalLength;
   public int iterations;

   private void Awake()
   {
      totalLength = 0;
      ComputeLengths();

      jointPoints = new List<Vector3>();
      
      foreach (var point in chain)
      {
         jointPoints.Add(point.joint.transform.position);
      }
      
      if(totalLength + (chain[0].joint.transform.position.magnitude) < (chain.Last().joint.transform.position - goalPoint.position).magnitude)
         print("ay caramba");
      
   }

   private void FixedUpdate()
   {
      IkYing();
   }

   private void ComputeLengths()
   {
      for (int i = 0; i < chain.Count - 1; i++)
      {
         chain[i].length = (chain[i].joint.transform.position - chain[i + 1].joint.transform.position).magnitude;
         totalLength += chain[i].length;
      }

      chain[chain.Count - 1].length = chain[chain.Count - 2].length;
      totalLength += chain[chain.Count - 1].length;
   }

   private void IkYing()
   {
      bool arrived = false;
      for (int i = 0; i < iterations && !arrived; i++)
      {
         Forwards();
         Backwards();

         if ((chain.Last().joint.transform.position - goalPoint.position).magnitude < marginOfError)
            arrived = true;
      }

      for (int i = 0; i < chain.Count; i++)
      {
         chain[i].joint.transform.position = jointPoints[i];
      }
      
      for (int j = 0; j < chain.Count - 1; j++)
      {
         var quat =
            Quaternion.LookRotation(chain[j + 1].joint.transform.position - chain[j].joint.transform.position);

         CheckForCorrectRotations(chain[j], quat);
      }
   }

   private void CheckForCorrectRotations(Point point, Quaternion quat)
   {
      var eulerAngles = point.joint.transform.parent.transform.eulerAngles;
      Vector3 angles = quat.eulerAngles - eulerAngles;

      if (point.constrainX)
         if (point.maxX == 0 && point.minX == 0)
            angles.x = 0;
         else
         {
            print(quat.eulerAngles);
            print("Clamping before " + angles.x);
            angles.x = Mathf.Clamp(angles.x, point.minX, point.maxX);
            print("Clamping after " + angles.x);
         }
            

      if (point.constrainY)
         if (point.maxY == 0 && point.minY == 0)
            angles.y = 0;
         else
            angles.y = Mathf.Clamp(angles.y, point.minY, point.maxY);

      if (point.constrainZ)
         if (point.maxZ == 0 && point.minZ == 0)
            angles.z = 0;
         else
            angles.z = Mathf.Clamp(angles.z, point.minZ, point.maxZ);
      
      point.joint.transform.eulerAngles = angles + eulerAngles;
   }

   private void Forwards()
   {
      jointPoints[jointPoints.Count - 1] = goalPoint.position;
      for (int i = jointPoints.Count - 2; i >= 0 ; i--)
      {
         Vector3 distance = jointPoints[i] - jointPoints[i + 1];
         Vector3 destination = (distance.normalized * chain[i + 1].length) + jointPoints[i + 1];

         jointPoints[i] = destination;
      }
   }

   private void Backwards()
   {
      jointPoints[0] = chain[0].joint.transform.position;
      for (int i = 1; i < jointPoints.Count; i++)
      {
         Vector3 distance = jointPoints[i]- jointPoints[i - 1];
         Vector3 destination = (distance.normalized * chain[i].length)  + jointPoints[i - 1];

         jointPoints[i] = destination;
         //jointPoints[i + 1].joint.transform.LookAt(chain[i].joint.transform);
      }
   }
}