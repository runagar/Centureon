using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTII : MonoBehaviour {
    public float _frontRigthLeg;
    public float _frontLeftLeg;
    public float _backRightLeg;
    public float _backLeftLeg;

    float _legDifferenceAB, _legDifferenceAC, _legDifferenceBD, _legDifferenceCD;
    private Vector3 _rotation;
    private float _rotationSensitivity;
    float deltaSideWays, deltaFrontWays;
    void Start () {
        _rotationSensitivity = 0.2f;
	}
	
	void Update () {
        deltaSideWays = (_frontLeftLeg + _backLeftLeg) - (_frontRigthLeg + _backRightLeg);
        deltaFrontWays = (_frontRigthLeg + _frontLeftLeg) - (_backRightLeg + _backLeftLeg);
        _rotation = Vector3.zero;
        if (Mathf.Abs(deltaSideWays) > 50)
        {
            //we lean right
            _rotation.y += deltaSideWays * _rotationSensitivity;
        }
        if(Mathf.Abs(deltaFrontWays) > 50)
        {
            _rotation.x += deltaFrontWays * _rotationSensitivity;
        }



    }
}
