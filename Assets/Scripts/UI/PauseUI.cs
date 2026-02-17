using UnityEngine;
using System.Collections;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePanel;
    public float animationSpeed = 6f;

    private bool isPaused = false;

    private Vector3 smallScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 normalScale = Vector3.one;

    public void OpenPause()
    {
        pausePanel.SetActive(true);
        pausePanel.transform.localScale = smallScale;

        Time.timeScale = 0f;
        StartCoroutine(ScalePanel(smallScale, normalScale));

        isPaused = true;

        AudioManager.Instance.PlayPauseOpen();
    }

    public void ClosePause()
    {
        Time.timeScale = 1f;
        StartCoroutine(ScalePanel(normalScale, smallScale));

        isPaused = false;

        AudioManager.Instance.PlayPauseClose();
    }

    IEnumerator ScalePanel(Vector3 start, Vector3 end)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * animationSpeed;
            float smoothT = t * t * (3f - 2f * t);

            pausePanel.transform.localScale = Vector3.Lerp(start, end, smoothT);
            yield return null;
        }

        pausePanel.transform.localScale = end;

        if (end == smallScale)
            pausePanel.SetActive(false);
    }
}
