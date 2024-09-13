using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerController : MonoBehaviour
{
    [SerializeField] private MushroomSpawner MushroomSpawner;
    [SerializeField] private Collider WalkArea;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Animator Animator;

    private Queue<GameObject> _mushroomList;
    private GameObject _currentMushroom;
    private Vector3 _destination;
    private bool _isEating;

    private void Start()
    {
        _mushroomList = new Queue<GameObject>();
    }

    private void OnEnable()
    {
        MushroomSpawner.OnMushroomSpawned += OnMushroomSpawned;
    }

    private void OnDisable()
    {
        MushroomSpawner.OnMushroomSpawned -= OnMushroomSpawned;
    }

    private void Update()
    {
        if (!_isEating)
        {
            Move();
        }
    }

    private void Move()
    {
        if (_currentMushroom == null)
        {
            if (_mushroomList.Count > 0)
            {
                _currentMushroom = _mushroomList.Dequeue();
                _destination = _currentMushroom.transform.position - ((_currentMushroom.transform.position - transform.position).normalized * 2);
            }
            else
            {
                bool isArrive = GoToDestination();
                if (isArrive)
                {
                    _destination = new Vector3(
                        Random.Range(WalkArea.bounds.min.x, WalkArea.bounds.max.x),
                        100,
                        Random.Range(WalkArea.bounds.min.z, WalkArea.bounds.max.z));
                }
            }
        }
        else
        {
            bool isArrive = GoToDestination();
            if (isArrive)
            {
                Eat();
            }
        }

        Animator.SetFloat("Move", Agent.velocity.sqrMagnitude);
    }

    public void OnAnimationEvent(string name)
    {
        if(name == "EatMushroom")
        {
            Destroy(_currentMushroom);
            _currentMushroom = null;
        }
        else if(name == "EatEnd")
        {
            _isEating = false;
            Animator.SetBool("IsEat", false);
        }
    }

    private void Eat()
    {
        _isEating = true;
        Animator.SetBool("IsEat", true);
    }

    private void OnMushroomSpawned(GameObject mushroom)
    {
        _mushroomList.Enqueue(mushroom);
        //Debug.Log($"Deer go to mushroom {mushroom.name} {mushroom.transform.position}");
    }

    private bool GoToDestination()
    {
        Agent.SetDestination(_destination);
        //Debug.Log($"pos:{transform.position} dest:{_destination} distance:{Agent.remainingDistance}");
        if ((Agent.remainingDistance <= Agent.stoppingDistance) &&
            (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f))
        {
            return true;
        }
        return false;
    }

    private void WalkRandom()
    {

    }
}