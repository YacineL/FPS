using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] float waitingTime = 0.5f;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public IEnumerator HandleDeath()
    {
        BroadcastMessage("MuteSFX");
        yield return new WaitForSeconds(waitingTime);
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
