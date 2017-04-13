using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTII : MonoBehaviour {
    public float _frontRigthLeg;
    public float _frontLeftLeg;
    public float _backRightLeg;
    public float _backLeftLeg;

    private Vector3 _rotation;
    private float _rotationSensitivity;
    public float deltaSideWays, deltaFrontWays;

    Arduino arduino;

    void Start () {
        _rotationSensitivity = 0.005f;
        arduino = GameObject.Find("Logger").GetComponent<Arduino>();
	}
	
	void Update () {

        _frontLeftLeg = arduino.FSR_FL;
        _frontRigthLeg = arduino.FSR_FR;
        _backLeftLeg = arduino.FSR_RL;
        _backRightLeg = arduino.FSR_RR;

        if (_frontLeftLeg < 0) _frontLeftLeg = 0;
        if (_frontRigthLeg < 0) _frontRigthLeg = 0;
        if (_backLeftLeg < 0) _backLeftLeg = 0;
        if (_backRightLeg < 0) _backRightLeg = 0;

        deltaSideWays = (_frontLeftLeg + _backLeftLeg) - (_frontRigthLeg + _backRightLeg);
        deltaFrontWays = (_frontRigthLeg + _frontLeftLeg) - (_backRightLeg + _backLeftLeg);
        _rotation = Vector3.zero;

        //determines which side the user is leaning to
        if (Mathf.Abs(deltaSideWays) > 10)
        {
            _rotation.y += deltaSideWays * _rotationSensitivity;
            transform.Rotate(Vector3.up * _rotation.y);
        }

        //determines if the user is leaning forward or backwards.
        if (Mathf.Abs(deltaFrontWays) > 10)
        {
            _rotation.x += deltaFrontWays * _rotationSensitivity;
            transform.Rotate(Vector3.right * _rotation.x);
        }

        //""""""""""""
        //check if we are at the rotaion limit.
        //""""""""""""

        if (transform.eulerAngles.x > 180 && transform.eulerAngles.x < 345) transform.eulerAngles = new Vector3(345, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.x > 15 && transform.eulerAngles.x < 180) transform.eulerAngles = new Vector3(15, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.y > 180 && transform.eulerAngles.y < 345) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 345, 0);
        if (transform.eulerAngles.y > 15 && transform.eulerAngles.y < 180) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 15, 0);

    }
}
