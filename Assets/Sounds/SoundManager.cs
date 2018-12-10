using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource source;
    public AudioClip Pickup;
    public AudioClip KaChing;
    public AudioClip QuestAccepted;
    public AudioClip EyeOfTheTiger;

    void Awake()
    {
        Pickup = Resources.Load<AudioClip>("Pickup");
        KaChing = Resources.Load<AudioClip>("KaChing");
        QuestAccepted = Resources.Load<AudioClip>("QuestAccepted");
        EyeOfTheTiger = Resources.Load<AudioClip>("EyeOfTheTiger");
        source = GetComponent<AudioSource>();
    }

    public void playPickup()
    {
        source.clip = Pickup;
        source.Play();
    }

    public void playKaChing()
    {
        source.clip = KaChing;
        source.Play();
    }

    public void playQuestAccepted()
    {
        source.clip = QuestAccepted;
        source.Play();
    }

    public void playEyeOfTheTiger()
    {
        source.clip = EyeOfTheTiger;
        source.Play();
    }
}
