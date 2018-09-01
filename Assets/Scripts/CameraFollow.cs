using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;

    private float smoothSpeed = 10.0f;


    private void LateUpdate() {
        Vector3 tPos = target.position + offset;
        transform.position = target.position;
        Vector3 sPos = Vector3.Lerp(transform.position, tPos, smoothSpeed * Time.deltaTime);
        transform.position = sPos;
    }
}
