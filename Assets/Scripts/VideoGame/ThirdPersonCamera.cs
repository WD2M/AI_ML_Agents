using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offSet;
    public Transform target;
    [Range (0, 1)] public float lerpValue;
    public float moveX;
    public float moveY;
    public float sencibilidad;

    void Update()
    {
        moveX = Input.GetAxis("Mouse X");
        moveY = Input.GetAxis("Mouse Y");
    }
    private void LateUpdate()
    {
        transform.Rotate((moveY * -1) * sencibilidad, moveX * sencibilidad, 0);
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y, 0f));

        transform.position = Vector3.Lerp(transform.position, target.position 
            + offSet, lerpValue);

    }
}
