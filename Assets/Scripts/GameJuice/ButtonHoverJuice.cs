using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverJuice : MonoBehaviour,
    IPointerEnterHandler, // Implementing Unity's pointer event interfaces
    IPointerExitHandler // Implementing Unity's pointer event interfaces
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
