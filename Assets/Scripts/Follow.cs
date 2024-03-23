using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public new Transform transform;

    // 타겟
    [SerializeField]
    private Transform target;

    // 프로퍼티
    [SerializeField]
    private float smoothSpeed;

    [SerializeField]
    private Vector3 offset;

    private IEnumerator Start()
    {
        while (true)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;

            yield return null;
        }
    }
}
