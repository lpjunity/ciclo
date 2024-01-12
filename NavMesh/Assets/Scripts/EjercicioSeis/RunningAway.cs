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
        /*  Calcular la distancia entre el agente y el personaje. Podéis usar la resta de vectores y calculo del módulo, o usar la función Distance
         *  Si la distancia es menor que un límite (podéis hacer enemigos más fáciles/difíciles de tocar), el agente se empezará a mover.
         *  
           	Si nos tenemos que mover, hay que calcular la dirección en la que moverse (restando vectores), y después calcular la nueva posición. 
            Si tenemos la dirección del vector, podemos multiplicarlo por una magnitud para hacer que se mueva.
            Una vez calculado el destino, asociarlo.
            Si la distancia es suficientemente grande, podéis parar al agente usando la función ResetPath()

         */
    }
}
