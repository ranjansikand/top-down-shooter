using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    public NavMeshAgent agent;

    public delegate void UpdateScore();
    public static UpdateScore addPoint;

    void Awake() 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            // add points to player score
            addPoint();

            // Instantiate particle effect

            Destroy(gameObject);
        }
    }
}
