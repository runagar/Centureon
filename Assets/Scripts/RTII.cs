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
    void Start () {
        _rotationSensitivity = 0.005f;
	}
	
	void Update () {
        deltaSideWays = (_frontLeftLeg + _backLeftLeg) - (_frontRigthLeg + _backRightLeg);
        deltaFrontWays = (_frontRigthLeg + _frontLeftLeg) - (_backRightLeg + _backLeftLeg);
        _rotation = Vector3.zero;
        //determines which side the user is leaning to
        if (Mathf.Abs(deltaSideWays) > 50)
        {
            _rotation.y += deltaSideWays * _rotationSensitivity;
            transform.Rotate(Vector3.up * _rotation.y);
        }
        else
        {
            _rotation.y = Mathf.MoveTowards(_rotation.y, 0, Time.deltaTime);
           
        }

        //determines if the user is leaning forward or backwards.
        if(Mathf.Abs(deltaFrontWays) > 50)
        {
            _rotation.x += deltaFrontWays * _rotationSensitivity;
            transform.Rotate(Vector3.right * _rotation.x);
        }
        else
        {
            _rotation.x = Mathf.MoveTowards(_rotation.x, 0, Time.deltaTime);
            
        }

        //""""""""""""
        //check if we are at the rotaion limit.
        //""""""""""""
        
        if (transform.eulerAngles.x < -15) transform.eulerAngles = new Vector3(-15, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.x > 15) transform.eulerAngles = new Vector3(15, transform.eulerAngles.y, 0);
        if (transform.eulerAngles.y < -15) transform.eulerAngles = new Vector3(transform.eulerAngles.x, -15, 0);
        if (transform.eulerAngles.y > 15) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 15, 0);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
