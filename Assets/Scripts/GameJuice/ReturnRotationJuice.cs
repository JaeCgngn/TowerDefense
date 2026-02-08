using UnityEngine;

public class ReturnRotationJuice : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private float speed = 5f;

    Quaternion original;
    bool returning;

    void Awake()
    {
        original = transform.rotation;

        if (!turret)
            turret = GetComponentInParent<Turret>();
    }

    void OnEnable()
    {
        turret.OnTargetLost += StartReturn;
    }

    void OnDisable()
    {
        turret.OnTargetLost -= StartReturn;
    }

    void StartReturn() => returning = true;

    void Update()
    {
        if (!returning) return;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            original,
            Time.deltaTime * speed
        );

        if (Quaternion.Angle(transform.rotation, original) < 0.1f)
            returning = false;
    }
}
