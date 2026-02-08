using UnityEngine;

public class AnticipationScaleJuice : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private Vector3 enlargedScale = new(1.2f, 1.2f, 1.2f);
    [SerializeField] private float speed = 6f;
    [SerializeField] private bool scaleOnlyY;

    Vector3 baseScale;
    Vector3 targetScale;

    void Awake()
    {
        baseScale = transform.localScale; // Store the original scale
        targetScale = baseScale; // Initialize target scale to the original scale

        if (!turret)
            turret = GetComponentInParent<Turret>();
    }

    void OnEnable()
    {
        turret.OnTargetAcquired += Enlarge;
        turret.OnTargetLost += ResetScale;
    }

    void OnDisable()
    {
        turret.OnTargetAcquired -= Enlarge;
        turret.OnTargetLost -= ResetScale;
    }

    void Enlarge()
    {
        targetScale = scaleOnlyY
          ? new Vector3(baseScale.x, Mathf.Max(transform.localScale.y, enlargedScale.y), baseScale.z)
          : new Vector3(
              Mathf.Max(transform.localScale.x, enlargedScale.x),
              Mathf.Max(transform.localScale.y, enlargedScale.y),
              Mathf.Max(transform.localScale.z, enlargedScale.z)
            );
    }

    void ResetScale()
    {
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
}
