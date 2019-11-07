using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    private float startTimeInBalance = Single.MinValue;

    public bool onTheGround = false;

    private void OnCollisionEnter(Collision other)
    {
        CCDIKController player = other.gameObject.transform.GetComponentInParent<CCDIKController>();
        ScoreElement scoreElement = other.gameObject.GetComponent<ScoreElement>();
        
        if ((player || ( scoreElement && !scoreElement.onTheGround)))
        {
            if (startTimeInBalance == Single.MinValue) 
                startTimeInBalance = Time.time;

        }
        else
        {
            TouchedGround();
        }
    }

    private void TouchedGround()
    {
        onTheGround = true;

        if (startTimeInBalance != Single.MinValue)
        {
            int score = (int) (Time.time - startTimeInBalance);
            GameManager.Instance.score += score;
        }
        
        
        //TODO: maybe add a dissolve effect
        Destroy(gameObject, 0.5f);
    }
}
