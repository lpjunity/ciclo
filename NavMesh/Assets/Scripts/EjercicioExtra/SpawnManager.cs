using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private List<GameObject> _monsterTypes;

    [SerializeField] private int _maxSpawn;
    [SerializeField] private float _timeUntilSpawn;

    private float _timePassed;
    private int _numberOfMonstersSpawn;

    private GameObject _player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if(_numberOfMonstersSpawn < _maxSpawn)
        {
            if(_timePassed > _timeUntilSpawn)
            {
                Debug.Log($"TimePassed: {_timePassed} + TimeUntilSpawn: {_timeUntilSpawn}");
                GameObject enemy = Instantiate(_monsterTypes[Random.Range(0, _monsterTypes.Count)], gameObject.transform);
                Following follow = enemy.GetComponent<Following>();
                if (follow)
                {
                    follow.Init(_player);
                }
                _numberOfMonstersSpawn++;
                _timePassed = 0;
            }
        }
        _timePassed += Time.deltaTime;
    }

    public void RegisterMonsterKilled()
    {
        _numberOfMonstersSpawn--;
    }
}
