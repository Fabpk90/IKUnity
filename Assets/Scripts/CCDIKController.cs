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

        basePostionLeftGoal = leftArmGoal.transform.localPosition;
        basePostionRightGoal = rightArmGoal.transform.localPosition;
    }

    private void OnRightArmMoved(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<Vector2>();
        
        val.x = Mathf.Clamp(val.x, -.55f, .65f);
        val.y = Mathf.Clamp01(val.y);
        
        rightArmGoal.transform.localPosition = new Vector3(val.x * rangeRightArm.x, 0, val.y * rangeRightArm.y) + basePostionRightGoal;
    }

    private void OnLeftArmMoved(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<Vector2>();

        val.x = Mathf.Clamp(val.x, -.55f, .65f);
        val.y = Mathf.Clamp01(val.y);
        
        leftArmGoal.transform.localPosition = new Vector3(val.x * rangeLeftArm.x, 0, val.y * rangeLeftArm.y) + basePostionLeftGoal;
    }
}