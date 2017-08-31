using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource bossMusic1;
    [SerializeField] private AudioSource bossMusic2;
    [SerializeField] private AudioSource bossMusic3;
    [SerializeField] private AudioSource endingMusic;


    // Use this for initialization
    void Start ()
    {
        backgroundMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void StopBackGroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayBossMusic1()
    {
        bossMusic1.Play();
    }

    public void StopBossMusic1()
    {
        bossMusic1.Stop();
    }

    public void PlayBossMusic2()
    {
        bossMusic2.Play();
    }

    public void StopBossMusic2()
    {
        bossMusic2.Stop();
    }

    public void PlayBossMusic3()
    {
        
        bossMusic3.Play();
    }

    public void StopBossMusic3()
    {
        bossMusic3.Stop();
    }

    public void PlayEndingMusic()
    {
        endingMusic.Play();
    }

    public void StopEndingMusic()
    {
        endingMusic.Stop();
    }
}
