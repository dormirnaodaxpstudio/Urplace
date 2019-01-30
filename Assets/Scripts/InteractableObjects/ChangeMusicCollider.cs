using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicCollider : MonoBehaviour
{
    [SerializeField] AudioSource themeMusic;
    [SerializeField] AudioSource endPianoMusic;
    [SerializeField] AudioSource endPianoMusic2;
    [SerializeField] Animator pianoAnimator;

    private bool changeMusic = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeMusic = true;
            pianoAnimator.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (changeMusic)
        {
            if (themeMusic.volume > 0f)
                themeMusic.volume -= 0.3f * Time.deltaTime;
            if (endPianoMusic.volume < 1f)
            {
                endPianoMusic.volume += 0.5f * Time.deltaTime;
                endPianoMusic2.volume += 0.5f * Time.deltaTime;
            }
                
        }
    }
}
