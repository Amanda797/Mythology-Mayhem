using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMirror : MythologyMayhem
{
    [SerializeField] bool testingBool;
    [Tooltip("Is the mirror in cooldown mode?")]
    [SerializeField] bool mirrorCoolDown = false;
    [Tooltip("How long should it take for the enemy to be slowed down/frozen?")]
    [SerializeField] float enemySlowDuration = 5f;
    [Tooltip("How long should the mirror's cooldown take?")]
    [SerializeField] float coolDownDuration = 5f;
    [SerializeField] float lastUsedTime;
    [SerializeField] Material normal;
    [SerializeField] Material stone;
    [SerializeField] ParticleSystem reflectionVFX;

    //reference to player camera to cast ray forward from (Allows crosshairs or centered enemy to what gets hit)
    public GameObject playerCamera;
    public WeaponSwitcher weaponSwitcher;

    void Update()
    {
        if(testingBool) {
            ActivateMirror();
            testingBool = false;
        }

        if(Input.GetMouseButtonDown(1) && weaponSwitcher.currentOffHand == OffHand.Mirror) {
            mirrorCoolDown = true;
            lastUsedTime = Time.time;
            ActivateMirror();
        }

        if (mirrorCoolDown)
        {
            if (coolDownDuration <= Time.time - lastUsedTime) 
            {
                mirrorCoolDown = false;
            }
        }
    }

    public void ActivateMirror()
    {
        Vector3 startPos = playerCamera.transform.position;

        Ray mirrorRay = new Ray(startPos, playerCamera.transform.forward);
        Debug.DrawRay(startPos, playerCamera.transform.forward * 100, Color.green, 0.1f);

        if (Physics.Raycast(mirrorRay, out RaycastHit hit))
        {
            if (hit.transform.gameObject.tag.Equals("Medusa"))
            {
                print("Hit: " + hit.transform.name);
                FreezeMedusa(hit.transform.gameObject);
            }
        }

    }//end ActivateMirror

    void FreezeMedusa(GameObject medusa) {
        //Freeze Medusa's Movements
        MedusaControlScript medusaControlScript = medusa.GetComponent<MedusaControlScript>();
        if (medusaControlScript.CurrentState == MedusaControlScript.AttackStates.AttemptToFreeze1)
        {
            reflectionVFX.Play();
            medusaControlScript.playerSuccessFreeze1 = true;
            medusaControlScript.mirror = this.gameObject;
        }
        if (medusaControlScript.CurrentState == MedusaControlScript.AttackStates.AttemptToFreeze2)
        {
            reflectionVFX.Play();
            medusaControlScript.playerSuccessFreeze2 = true;
            medusaControlScript.mirror = this.gameObject;
        }
        if (medusaControlScript.CurrentState == MedusaControlScript.AttackStates.AttemptToFreeze3)
        {
            reflectionVFX.Play();
            medusaControlScript.playerSuccessFreeze3 = true;
            medusaControlScript.mirror = this.gameObject;
        }
        StartCoroutine(MirrorCooldown());
        //StartCoroutine(MedusaCooldown(medusa));
    }//end FreezeMedusa

    IEnumerator MirrorCooldown() {
        yield return new WaitForSeconds(coolDownDuration);
        mirrorCoolDown = false;
    }//end cooldown coroutine

    IEnumerator MedusaCooldown(GameObject medusa) {
        yield return new WaitForSeconds(enemySlowDuration);
       // medusa.GetComponent<MedusaControlScript>().SetAggroState(MedusaControlScript.AggroStates.Normal);
        //medusa.GetComponent<MeshRenderer>().material = normal;
    }//end cooldown coroutine
}
