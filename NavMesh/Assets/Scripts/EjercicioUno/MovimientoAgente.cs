using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Hay que incluir esta dependencia!
using UnityEngine.AI;

public class MovimientoAgente : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform destino;
    void Start()
    {
        /* TODO: Obtener una referencia al agente, y establecer como destino las coordenadas del objeto 'Premio' */
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destino.position;

    }

    // ¿Hace falta poner el Update?
    void Update()
    {
        
    }
}
