using Cinemachine;
using UnityEngine;

public class ChangeToCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camera.MoveToTopOfPrioritySubqueue();
        }

    }
}
