using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverJuice : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] float hoverScale = 1.1f;
    [SerializeField] float speed = 10f;

    Vector3 baseScale;
    Vector3 targetScale;

    void Awake()
    {
        baseScale = transform.localScale;
        targetScale = baseScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = baseScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = baseScale;
    }
}
