using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject heart;
    [SerializeField] private AudioSource heartBeatSound;

    private float firstDistanceBetweenPlayerAndHeart;
    private float currentDistanceBetweenPlayerAndHeart;


    private void Awake()
    {
        firstDistanceBetweenPlayerAndHeart = CalculatePlayerToHeartDistance();
    }

    void Update()
    {
        currentDistanceBetweenPlayerAndHeart = CalculatePlayerToHeartDistance();
        
        heartBeatSound.volume = 1f - ((currentDistanceBetweenPlayerAndHeart/1000f)/(firstDistanceBetweenPlayerAndHeart/1000f)); 
    }

    public float CalculatePlayerToHeartDistance()
    {
        return (heart.transform.position.x - player.transform.position.x);
    }
}
