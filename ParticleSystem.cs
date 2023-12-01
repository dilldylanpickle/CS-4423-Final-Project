using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolParticleSystem : MonoBehaviour
{
    public ParticleSystem backgroundParticleSystem;
    private static CoolParticleSystem instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            backgroundParticleSystem.Play();
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }
}