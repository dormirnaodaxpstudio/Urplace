using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
	public float timer;

    public bool end;

    private Color startColor;
    private Color alphaColor;
    private Color desiredColor;

	void Start () {
        startColor = this.GetComponent<Image>().color;
        alphaColor = startColor;
        alphaColor.a = 0;
    }

	void Update () {
        if (timer > 0) {
            timer -= Time.deltaTime;
            this.GetComponent<Image>().color = Color.Lerp(this.GetComponent<Image>().color, desiredColor, Time.deltaTime / timer);
        } else if (timer < 0) timer = 0;
    }

    public void Fade(float duration) {
        if (duration < 0) {
            desiredColor = alphaColor;
            timer = -duration;
        } else {
            desiredColor = startColor;
            desiredColor.a = 1.05f;
            timer = duration;
        }
		StartCoroutine(DelayLoading(duration));
    }

	IEnumerator DelayLoading (float time) {
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(2);
	}
}
