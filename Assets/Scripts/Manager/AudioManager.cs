using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    [Header("Attack Sounds")]
    public AudioClip[] attackSFX;

    public AudioClip upgradeSFX;

    public AudioClip UIOpenSFX;

    public AudioClip UICloseSFX;

    public AudioClip[] CountdownSFX;

    public AudioClip insertTurretSFX;

    public AudioClip GameOverSFX;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }


    public void PlayAttack()
    {
        if (attackSFX.Length == 0) return;

        int index = Random.Range(0, attackSFX.Length);
        audioSource.PlayOneShot(attackSFX[index]);
    }

        public void PlayCountdown()
    {
        if (CountdownSFX.Length == 0) return;

        int index = Random.Range(0, CountdownSFX.Length);
        audioSource.PlayOneShot(CountdownSFX[index]);
    }

    public void PlayPauseOpen()
    {
        audioSource.PlayOneShot(UIOpenSFX);
    }

    public void PlayPauseClose()
    {
        audioSource.PlayOneShot(UICloseSFX);
    }

    public void PlayUpgrade()
    {
        audioSource.PlayOneShot(upgradeSFX);
    }

    public void PlayInsertTurret()
    {
        audioSource.PlayOneShot(insertTurretSFX);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(GameOverSFX);
    }

}
