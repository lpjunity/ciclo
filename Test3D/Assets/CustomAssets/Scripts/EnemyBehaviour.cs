using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _movementSpeed/100);

    }

    private void OnCollisionEnter(Collision collision)
    {
        _player.SetActive(false);
        GameManager.Instance.HandleDefeat();
        Destroy(_player);
    }
}
