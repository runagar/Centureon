using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTII : MonoBehaviour {
    public float _backLeftLeg, _backRightLeg, _frontLeftLeg,_frontRigthLeg;

    private Vector3 _rotation;
    private float _rotationSensitivity, sideTemp, frontTemp;
    public float deltaSideWays, deltaFrontWays;
    void Start () {
        _rotationSensitivity = 0.06f;
	}
	
	void Update () {
        deltaSideWays = (_frontLeftLeg + _backLeftLeg) - (_frontRigthLeg + _backRightLeg);
        deltaFrontWays = (_frontRigthLeg + _frontLeftLeg) - (_backRightLeg + _backLeftLeg);
      //  _rotation = Vector3.zero;
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0); ;

        if (Mathf.Abs(deltaSideWays) > 50)
        {
            sideTemp = deltaSideWays / 60;
           // _rotation += new Vector3( 0,sideTemp * _rotationSensitivity,0);
            transform.rotation = transform.rotation * Quaternion.Euler(sideTemp, transform.eulerAngles.y, 0);
        }
        if(Mathf.Abs(deltaFrontWays) > 50)
        {
            frontTemp = deltaFrontWays / 60;
            //  _rotation += new Vector3(frontTemp * _rotationSensitivity,0,0);
            transform.rotation = transform.rotation * Quaternion.Euler(transform.eulerAngles.x, frontTemp, 0);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        if (transform.eulerAngles.x < -15)
            transform.eulerAngles = new Vector3(-15, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.x > 15)
            transform.eulerAngles = new Vector3(15, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.y < -15)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -15, 0);
        if (transform.eulerAngles.y > 15)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 15, 0);
    }
}
