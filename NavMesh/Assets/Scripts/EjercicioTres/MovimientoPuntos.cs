using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoPuntos : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] listaVectores;

    private NavMeshAgent agente;
    private GameObject _player;

    int actual = 0;
    public float _rayDistance;
    private bool _following;

    private void Start()
    {
        /* TODO: Crear la lista de puntos (se puede hacer por código o desde la UI*/
        /* TODO: Establecer el primer punto como destino */
        agente = GetComponent<NavMeshAgent>();
        agente.destination = listaVectores[actual].position;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /* El algoritmo es algo así:
         *  1 - Si hemos llegado al destino (es decir, la x y z de mi transform es igual al destino establecido en el agente)
         *  2 - Busco cual es el siguiente punto, y lo establezco como destino. ¿Qué pasa si hemos llegado al último punto?
         */
        if(_following)
        {
            agente.destination = _player.transform.position;
        }
        
        float distance = Vector3.Distance(agente.transform.position, agente.destination);

        if (distance <= .5f)
        {
            actual++;
            agente.destination = listaVectores[actual%listaVectores.Length].position;
            _following = false;
        }

       Vector3 origin = agente.transform.position;
       Vector3 direction = agente.transform.forward;
       RaycastHit hit;
       if (Physics.Raycast(origin, direction, out hit))
       {
           Debug.DrawRay(origin, direction * _rayDistance, Color.red);
           if(!_following && hit.collider.CompareTag("Player"))
           {
                _following = true;
           }
       }
    }
}
