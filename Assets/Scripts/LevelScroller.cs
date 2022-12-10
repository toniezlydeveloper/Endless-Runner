using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Transform level;

    private void Update()
    {
        level.position += Vector3.back * (scrollSpeed * Time.deltaTime);
    }
}