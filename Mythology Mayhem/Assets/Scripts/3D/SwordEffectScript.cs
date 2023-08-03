using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffectScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem sparks1;
    [SerializeField] private ParticleSystem sparks2;
    [SerializeField] private ParticleSystem fireball;

    public Animator vfxAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(string whichParticle) 
    {
        switch (whichParticle)
        {
            case "Sparks1":
                sparks1.gameObject.SetActive(true);
                sparks1.Play();
                break;
            case "Sparks2":
                sparks2.gameObject.SetActive(true);
                sparks2.Play();
                break;
            case "Fireball":
                fireball.gameObject.SetActive(true);
                fireball.Play();
                break;
        }
    }

    public void HideParticles() 
    {
        sparks1.gameObject.SetActive(false);
        sparks2.gameObject.SetActive(false);
        fireball.gameObject.SetActive(false);
    }
}
