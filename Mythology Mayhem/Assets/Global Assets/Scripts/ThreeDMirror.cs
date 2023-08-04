using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMirror : MonoBehaviour
{
    [SerializeField] bool testingBool;
    [SerializeField] bool canUseMirror;
    [Tooltip("Is the mirror in cooldown mode?")]
    [SerializeField] bool mirrorCoolDown = false;
    [Tooltip("How long should it take for the enemy to be slowed down/frozen?")]
    [SerializeField] float enemySlowDuration = 5f;
    [Tooltip("How long should the mirror's cooldown take?")]
    [SerializeField] float coolDownDuration = 5f;
    [SerializeField] Material normal;
    [SerializeField] Material stone;

    void Update()
    {
        if(testingBool) {
            canUseMirror = true;
            ActivateMirror();
            testingBool = false;
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            ActivateMirror();
        }
    }

    public void ActivateMirror() {
        if(canUseMirror && !mirrorCoolDown){
            Vector3 mirrorPos = gameObject.transform.position;

            Ray mirrorRay = new Ray(mirrorPos, gameObject.transform.forward * -1); //-1 bc the forward is backwards

            if(Physics.Raycast(mirrorRay, out RaycastHit hit)) {
                if(hit.transform.gameObject.tag.Equals("Medusa")) {
                    print("Hit: " + hit.transform.name);
                    FreezeMedusa(hit.transform.gameObject);
                    mirrorCoolDown = true;
                    canUseMirror = false;
                }
            }
        }
    }//end ActivateMirror

    void FreezeMedusa(GameObject medusa) {
        //Freeze Medusa's Movements

        //medusa.GetComponent<MedusaControlScript>().SetAggroState(MedusaControlScript.AggroStates.Stone);
        medusa.GetComponent<MeshRenderer>().material = stone;
        StartCoroutine(MirrorCooldown());
        StartCoroutine(MedusaCooldown(medusa));
    }//end FreezeMedusa

    IEnumerator MirrorCooldown() {
        yield return new WaitForSeconds(coolDownDuration);
        mirrorCoolDown = false;
        canUseMirror = true;
    }//end cooldown coroutine

    IEnumerator MedusaCooldown(GameObject medusa) {
        yield return new WaitForSeconds(enemySlowDuration);
        //medusa.GetComponent<MedusaControlScript>().SetAggroState(MedusaControlScript.AggroStates.Normal);
        medusa.GetComponent<MeshRenderer>().material = normal;
    }//end cooldown coroutine
}
