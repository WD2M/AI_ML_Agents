using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTarget : MonoBehaviour
{
    public float timeChange;
    public Transform[] location;
    public int posActual;
    private void Start()
    {
        ChangeLocation();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mlagent"))
        {
            Invoke("ChangeLocation", timeChange);
        }
    }
    public void ChangeLocation()
    {
        int posNew = Random.Range(0, location.Length);
        if (posActual != posNew)
        {
            transform.position = location[posNew].position;
            posActual = posNew;
        }
        else
        {
            ChangeLocation();
        }
    }
}
