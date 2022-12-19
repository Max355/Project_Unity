using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPickup : MonoBehaviour
{
    [SerializeField] AudioClip chestPickupSFX;
    [SerializeField] int pointsForMoneyPickup = 100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForMoneyPickup);
            AudioSource.PlayClipAtPoint(chestPickupSFX, Camera.main.transform.position);//instantiate the audio, when we destroy this object it will still stick around
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
