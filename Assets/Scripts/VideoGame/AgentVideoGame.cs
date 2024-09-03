using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;

public class AgentVideoGame : Agent
{
    [Header("Velocidad")]
    [Range(0f, 5f)]
    public float _speed;
    [Header("Velocidad de giro")]
    [Range(50f, 500f)]
    public float _turnSpeed;
    //public bool _training = true;
    private Rigidbody _rb;
    [SerializeField]
    private Transform _target;
    private Vector3 _previous;
    private Vector3 previous;

    public float puntuacion;

    //public Transform[] location;

    //public float distance;
    //public float oldDistance;

    public Animator animator;
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _previous = transform.position;
        previous = transform.position;
        //MaxStep forma parte de la clase Agent
        //if (!_training) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        MoverPosicionInicial();
        _target.GetComponent<ControllerTarget>().ChangeLocation();
        _previous = transform.position;
    }
    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        float lForward = vectorAction.DiscreteActions[0];
        float lTurn = 0;
        if (vectorAction.DiscreteActions[1] == 1)
        {
            lTurn = -1;
        }
        else if (vectorAction.DiscreteActions[1] == 2)
        {
            lTurn = 1;
        }
        _rb.MovePosition(transform.position +
            transform.forward * lForward * _speed * Time.deltaTime);
        transform.Rotate(transform.up * lTurn * _turnSpeed * Time.deltaTime);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        //Distancia al target.
        //Float de 1 posicion.
        //distance = Vector3.Distance(_target.transform.position, transform.position);
        sensor.AddObservation(Vector3.Distance(_target.transform.position, transform.position));
        //Dirección al target.
        //Vector 3 posiciones. 
        sensor.AddObservation(
            (_target.transform.position - transform.position).normalized);
        //Vector del señor, donde mira.
        //Vector de 3 posiciones. 
        sensor.AddObservation(
            transform.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (_training)
        {
            if (other.CompareTag("target"))
            {
                animator.SetBool("baile", true);
                //puntuacion += 0.5f;
                //AddReward(0.5f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("target"))
        {
            animator.SetBool("baile", false);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("borders"))
    //    {
    //        puntuacion -= 0.2f;
    //        AddReward(-0.2f);
    //    }
    //}
    private void MoverPosicionInicial()
    {
        transform.position = previous;
        //int posNew = UnityEngine.Random.Range(0, location.Length);
        //if (_target.GetComponent<ControllerTarget>().posActual != posNew)
        //{
        //    transform.position = location[posNew].position;
        //}
        //else
        //{
        //    MoverPosicionInicial();
        //}
    }
}
