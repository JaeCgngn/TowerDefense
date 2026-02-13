using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenRedFlash : MonoBehaviour
{

    [SerializeField] private GameObject redOverlay;
    [SerializeField] private float duration;
    [SerializeField] private float flashIntensity;

    private Image redOverlayImage;
    private Coroutine flashCoroutine;


    void Start()
    {
        redOverlayImage = redOverlay.GetComponent<Image>();
        
    }

     private void OnEnable()
    {
            FollowRoute.OnRouteFinished += FlashRed;
    }

    private void OnDisable()
    {
            FollowRoute.OnRouteFinished -= FlashRed;
    }


    public void FlashRed()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRedCoroutine());
    }

    private IEnumerator FlashRedCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            float alpha = Mathf.Lerp(0f, flashIntensity, elapsed / (duration / 2f));
            redOverlayImage.color = new Color(redOverlayImage.color.r, redOverlayImage.color.g, redOverlayImage.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            float alpha = Mathf.Lerp(flashIntensity, 0f, elapsed / (duration / 2f));
            redOverlayImage.color = new Color(redOverlayImage.color.r, redOverlayImage.color.g, redOverlayImage.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        redOverlayImage.color = new Color(redOverlayImage.color.r, redOverlayImage.color.g, redOverlayImage.color.b, 0f);
    }
}
