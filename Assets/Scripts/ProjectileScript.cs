using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    void Awake() 
    {
        Invoke("DeleteSelf", 2f);
    }

    void OnCollisionEnter(Collision other)
    {
        DeleteSelf();
    }

    void DeleteSelf()
    {
        Destroy(gameObject);
    }
}