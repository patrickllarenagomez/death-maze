using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour {

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.00f)]
    public float smoothFactor;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationSpeed = 5.0f;


	// Use this for initialization
	void Start () {
        _cameraOffset = transform.position - PlayerTransform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (RotateAroundPlayer)
        {
            Quaternion cameraTurnAngle = 
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

            _cameraOffset = cameraTurnAngle * _cameraOffset;
        }


        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
	}
}
