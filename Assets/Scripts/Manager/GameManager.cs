using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coins;

    public static GameManager instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        instance = this;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
}
