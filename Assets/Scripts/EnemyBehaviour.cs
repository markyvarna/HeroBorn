using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//We gain access to NavMeshAgent by using UnityEngine.AI
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    //used to keep track of where the enemy currently is
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private int _lives = 3;
    public int EnemyLives
    {
        get
        {
            return _lives;
        }
        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Down");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializedPatrolRoute();
        MoveToNextPatrolLocation();
        
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializedPatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player Detected - ATTACK!!");
            agent.destination = player.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range - resume patrol.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // it's "Bullet(Clone)" because when instantiating new objects
        // unity will automatically add the suffix (Clone)
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }

}
