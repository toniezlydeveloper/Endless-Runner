using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [SerializeField] private float minZPosition;
    [SerializeField] private Transform[] grounds;

    private const float WorldSizeToScaleRatio = 10f;
    
    private void Update()
    {
        foreach (var ground in grounds)
        {
            if (ground.position.z < -minZPosition)
            {
                ground.position += Vector3.forward * (ground.localScale.z * WorldSizeToScaleRatio * grounds.Length);
            }
        }
    }
}