using UnityEngine;

public class IdleFloatJuice : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationAmount = 2f;

    Vector3 startPos;
    Quaternion startRot;
    bool active;
    float time;

    void Awake()
    {
        startPos = transform.position; // Store the original position
        startRot = transform.rotation; // Store the original rotation

        if (!turret)
            turret = GetComponentInParent<Turret>();
    }

    void OnEnable()
    {
        turret.OnTargetLost += Enable;
        turret.OnTargetAcquired += Disable;
    }

    void OnDisable()
    {
        turret.OnTargetLost -= Enable;
        turret.OnTargetAcquired -= Disable;
    }

    void Enable()
    {
        time = 0f;
        active = true;
    }

    void Disable()
    {
        active = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }

    void LateUpdate()
    {
        if (!active) return;

        time += Time.deltaTime;

        float y = Mathf.Sin(time * speed) * amplitude;
        float rot = Mathf.Sin(time * speed) * rotationAmount;

        transform.position = startPos + Vector3.up * y;
        transform.rotation = startRot * Quaternion.Euler(0f, rot, 0f);
    }
}
