using System.Collections;
using TMPro;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] float waitingTime = 0.5f;

    

    public IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(waitingTime);
        deathText.text = "You Die";
        AudioListener.pause = true;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
