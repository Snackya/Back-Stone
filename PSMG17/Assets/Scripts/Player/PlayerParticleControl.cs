using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleControl : MonoBehaviour {

    private Transform particles;
    private Transform healingParticles;
    private Transform healingEffect;

    private void Awake()
    {
        particles = transform.FindChild("Particles");
        healingParticles = particles.FindChild("HealingParticles");
        healingParticles.GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RevivalFountain")
        {
            healingParticles.GetComponent<ParticleSystem>().Play();
        }
    }

}
