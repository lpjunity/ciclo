using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningAway : MonoBehaviour
{
    [SerializeField] private GameObject _targetToRunAwayFrom;
    [SerializeField] private float _runLimit;
    [SerializeField] private float _stopDistance;
    private NavMeshAgent _agent;
    private bool _runningAway;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_agent.transform.position, _targetToRunAwayFrom.transform.position);
        if(distance <= _runLimit)
        {
            _runningAway = true;
        }

        if(distance >= _stopDistance) {
            _runningAway = false;
            _agent.ResetPath();
        }

        if(_runningAway)
        {
            Vector3 offset = (_agent.transform.position - _targetToRunAwayFrom.transform.position).normalized;
            _agent.Move(offset * Time.deltaTime);

            //Vector3 direction = (_agent.transform.position - _targetToRunAwayFrom.transform.position);
            //_agent.destination = direction * 5f;

        }
        /*  Calcular la distancia entre el agente y el personaje. Pod�is usar la resta de vectores y calculo del m�dulo, o usar la funci�n Distance
         *  Si la distancia es menor que un l�mite (pod�is hacer enemigos m�s f�ciles/dif�ciles de tocar), el agente se empezar� a mover.
         *  
           	Si nos tenemos que mover, hay que calcular la direcci�n en la que moverse (restando vectores), y despu�s calcular la nueva posici�n. 
            Si tenemos la direcci�n del vector, podemos multiplicarlo por una magnitud para hacer que se mueva.
            Una vez calculado el destino, asociarlo.
            Si la distancia es suficientemente grande, pod�is parar al agente usando la funci�n ResetPath()

         */
    }
}
