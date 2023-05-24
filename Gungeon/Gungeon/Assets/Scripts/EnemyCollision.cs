using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnCollisionEnter (UnityEngine.Collision collisionInfo)
    {
        Debug.Log(collisionInfo.collider.tag);
    }
}
