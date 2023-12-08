using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlParticles : MonoBehaviour
{
    public EventController eC;
    public ParticleSystem particles;

    public void PlayParticles()
    {
        particles.Play();
    }
}
