using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayerHealthManager : MonoBehaviour
{
    public int playerhealth = 10;

    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel; 

    void Start()
    {
        PlayerUpdateHealthUI();
    }

    private void OnEnable()
    {
        FollowRoute.OnRouteFinished += HandleRouteFinished;
    }

    private void OnDisable()
    {
        FollowRoute.OnRouteFinished -= HandleRouteFinished;
    }

    private void HandleRouteFinished()
    {
        Debug.Log("Event detected: Enemy reached end!");
        playerhealth -= 1;
        Debug.Log("Player health: " + playerhealth);
        PlayerUpdateHealthUI();
        // Camera.main.GetComponent<CameraShake>().Shake(0.2f, 0.3f);

    }

    public void PlayerUpdateHealthUI()
    {
        healthText.text = "Health: " + playerhealth;
    }

    public void PlayerDeath()
    {
            if (playerhealth <= 0)
            {
                Debug.Log("Player has died!");
                gameOverPanel.SetActive(true);
                
            }
    }
}

