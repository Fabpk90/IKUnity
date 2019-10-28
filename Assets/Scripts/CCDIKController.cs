using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CCDIKController : MonoBehaviour
{
    public Transform leftArmGoal;
    public Vector2 rangeLeftArm;
    
    public Transform rightArmGoal;
    public Vector2 rangeRightArm;

    private PlayerInput input;

    private Vector3 basePostionRightGoal;
    private Vector3 basePostionLeftGoal;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        input.currentActionMap["LeftArm"].performed += OnLeftArmMoved;
        input.currentActionMap["RightArm"].performed += OnRightArmMoved;

        basePostionLeftGoal = leftArmGoal.transform.position;
        basePostionRightGoal = rightArmGoal.transform.position;
    }

    private void OnRightArmMoved(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<Vector2>();
        
        rightArmGoal.transform.position = new Vector3(val.x * rangeRightArm.x, 0, val.y * rangeRightArm.y) + basePostionRightGoal;
    }

    private void OnLeftArmMoved(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<Vector2>();
        print(val);
        
        leftArmGoal.transform.position = new Vector3(val.x * rangeLeftArm.x, 0, val.y * rangeLeftArm.y) + basePostionLeftGoal;
    }
}