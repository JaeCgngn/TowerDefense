using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Vector3 originalPosition;
    
    void Awake()
    {
        originalPosition = transform.localPosition;
    }

      private void OnEnable()
    {
            FollowRoute.OnRouteFinished += ShakeWrapper;
    }

    private void OnDisable()
    {
            FollowRoute.OnRouteFinished -= ShakeWrapper;
    }

   private void ShakeWrapper()
    {
        Shake(0.2f, 0.3f); 
    }

    public void Shake(float duration, float magnitude)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
