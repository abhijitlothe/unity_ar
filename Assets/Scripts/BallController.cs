using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BallController : MonoBehaviour
{
	public Camera ARCamera;
    Vector3 _offset;
    Vector3 BaseRotation;
    Vector3 CameraOffset;
    private void Awake()
    {
        _offset = transform.position;
        BaseRotation = transform.eulerAngles;
        CameraOffset = BaseRotation - ARCamera.transform.eulerAngles;
    }

    private void Update()
    {
		Quaternion RotationOffset = Quaternion.Euler(BaseRotation) * Quaternion.Inverse(Camera.main.transform.rotation);
		RotationOffset = Quaternion.Euler(CameraOffset) * RotationOffset;
        transform.rotation = RotationOffset;
        Vector3 euler = transform.rotation.eulerAngles;
        euler.x = 360 - euler.x;
        euler.z = 360 - euler.z;
        transform.rotation = Quaternion.Euler(euler);
	}
}
