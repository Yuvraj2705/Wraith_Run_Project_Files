using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [Header("Shake Settings")]
    private Animator anim;
    [SerializeField] float Intensity;
    [SerializeField] float ShakeTime;

    [Header("Audio Files")]
    [SerializeField] AudioClip Hurt;
    [SerializeField] AudioClip Crystal;

    private AudioSource adSource;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        adSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            CMShakeScript.Instance.CameraShake(Intensity,ShakeTime);
            FindObjectOfType<LevelSettings>().Life -= 1;
            anim.SetTrigger("Hurt");
            adSource.PlayOneShot(Hurt);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Coin")
        {
            FindObjectOfType<LevelSettings>().CoinValue += 1;
            adSource.PlayOneShot(Crystal);
            Destroy(other.gameObject);
        }
    }
}
