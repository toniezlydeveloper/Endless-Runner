using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float minZPosition;

    private void Update()
    {
        if (transform.position.z < minZPosition)
        {
            Destroy(gameObject);
        }
    }
}