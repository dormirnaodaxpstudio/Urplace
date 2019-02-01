using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicCollider : MonoBehaviour
{
    [SerializeField] AudioSource themeMusic;
    [SerializeField] AudioSource endPianoMusic;
    [SerializeField] AudioSource endPianoMusic2;
    [SerializeField] Animator pianoAnimator;

    private GameObject character;

    private bool changeMusic = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character = other.gameObject;
            changeMusic = true;
            pianoAnimator.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (changeMusic && character.transform.position.x < 546f) //no final esse if nao pode mais rodar, para abaixar a música no final do crédito.
        {
            if (themeMusic.volume > 0f)
                themeMusic.volume -= 0.3f * Time.deltaTime;
            if (endPianoMusic.volume < 0.9f)
            {
                endPianoMusic.volume += 0.45f * Time.deltaTime;
                endPianoMusic2.volume += 0.45f * Time.deltaTime;
            }
                
        }
    }
}
