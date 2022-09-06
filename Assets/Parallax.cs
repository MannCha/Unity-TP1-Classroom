using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 lastCameraPosition;

    private void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float parallaxEffectMultiplier = .5f;
        transform.position += new Vector3((deltaMovement.x * parallaxEffectMultiplier), deltaMovement.y, 0);
        lastCameraPosition = cameraTransform.position;
        var test = transform.position - new Vector3(transform.position.x-0.1f * Time.deltaTime, transform.position.y, transform.position.z);
        //print($"My position is {transform.position}, my new position is {test}, subtraction equals {transform.position - test}.");
        //transform.localPosition -= new Vector3(transform.position.x-5, transform.position.y, transform.position.z);
        transform.position = transform.position - test;
    }
}
