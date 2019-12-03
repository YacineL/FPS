using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] Canvas impactCanvas;
    [SerializeField] float impactTime = 0.3f;
    [SerializeField] Sprite[] images = null;
    [SerializeField] float fadeOutTime = 0.3f;
    [SerializeField] float fadeInTime = 1f;

    Fader fader;

    int index;
    void Start()
    {
        impactCanvas.enabled = false;
        fader = impactCanvas.GetComponent<Fader>();
    }

    public void ShowDamageImpact()
    {
        index = Random.Range(0, images.Length);
        StartCoroutine(ShowSplatter(index));
    }

    IEnumerator ShowSplatter(int i)
    {
        impactCanvas.GetComponentInChildren<Image>().sprite = images[i];
        impactCanvas.enabled = true;
        yield return fader.FadeOut(fadeOutTime);
        yield return new WaitForSeconds(impactTime);
        yield return fader.FadeIn(fadeInTime);
        impactCanvas.enabled = false;
    }
}

