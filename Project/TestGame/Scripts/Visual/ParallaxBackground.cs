using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Vector3 targetPosition;

    [SerializeField] private Transform target;
    [SerializeField, Range(0, 1)] private float parallaxStrenght = 0.1f;

    private void Start()
    {
        target = Camera.main.transform;

        targetPosition = target.position;
    }

    private void Update()
    {
        Vector3 delta = target.position - targetPosition;

        delta.y = 0;

        targetPosition = target.position;
        transform.position += delta * parallaxStrenght;
    }
}