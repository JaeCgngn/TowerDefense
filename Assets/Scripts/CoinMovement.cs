using UnityEngine;

public class CoinMovement : MonoBehaviour
{

    public int coinValue = 5;
    public float duration = 0.7f;
    public Transform coinTarget;

    private Vector3 startPos;

    private float timer;


    void Awake()
    {

    }
    void Start()
    {
        startPos = transform.position;

        if (coinTarget == null) // If no target assigned, find the CoinTarget in the scene
        {
            coinTarget = GameObject.FindWithTag("CoinTarget").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        t = 1f - Mathf.Pow(1f - t, 3f);

        transform.position = Vector3.Lerp(startPos, coinTarget.position, t);

        if (timer >= duration)
        {
            ReachTarget();
        }
    }


    void ReachTarget()
    {
        PlayerStats.Instance.AddMoney(coinValue);
        Destroy(gameObject);
    }
}
