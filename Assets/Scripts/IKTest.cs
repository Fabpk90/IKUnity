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
      for (int i = 0; i < chain.Count; i++)
      {
         chain[i].length = 4;
         totalLength += chain[i].length;
      }
   }

   private void IkYing()
   {
      bool arrived = false;
      for (int i = 0; i < iterations && !arrived; i++)
      {
         Forwards();
         Backwards();

         if ((chain.Last().joint.transform.position - goalPoint.position).magnitude < marginOfError)
         {
            print("ay");
            arrived = true;
         }
      }

      for (int i = 0; i < chain.Count; i++)
      {
         chain[i].joint.transform.position = jointPoints[i];
      }
   }

   private void Forwards()
   {
      jointPoints[jointPoints.Count - 1] = goalPoint.position;
      for (int i = jointPoints.Count - 2; i >= 1 ; i--)
      {
         Vector3 distance = jointPoints[i] - jointPoints[i - 1];
         Vector3 destination = distance.normalized * chain[i - 1].length;

         jointPoints[i] = destination;
      }
   }

   private void Backwards()
   {
      jointPoints[0] = chain[0].joint.transform.position;
      for (int i = 0; i < jointPoints.Count - 2 ; i++)
      {
         Vector3 distance = jointPoints[i]- jointPoints[i + 1];
         Vector3 destination = distance.normalized * chain[i].length;

         jointPoints[i + 1] = destination;
         //jointPoints[i + 1].joint.transform.LookAt(chain[i].joint.transform);
      }
   }
}