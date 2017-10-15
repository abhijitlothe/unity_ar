using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTransform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.LogFormat("{6}=>RX: {0}, RY:{1} , RZ: {2}, PX: {3}, PY: {4} PZ: {5}", transform.localRotation.eulerAngles.x,
                        transform.localRotation.eulerAngles.y,
                        transform.localRotation.eulerAngles.z,
                        transform.position.x,
                        transform.position.y,
                        transform.position.z,
                        gameObject.name);
	}
}
