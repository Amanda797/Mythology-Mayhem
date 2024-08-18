using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Special Animations")]
    [SerializeField] GameObject boarCloudAnimation;
    float chargingTimer;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();

        if (boarCloudAnimation != null)
        {
            boarCloudAnimation.gameObject.SetActive(false);
            chargingTimer = enemy.attackRate;
        }
    }

    public void ChargingCloud()
    {
        if (enemy.CanAttack) StartCoroutine(IChargingCloud());
        else chargingTimer += 1f * Time.deltaTime;
    }

    IEnumerator IChargingCloud()
    {
        chargingTimer = 0f;
        boarCloudAnimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(enemy.attackRate);
        boarCloudAnimation.gameObject.SetActive(false);
    }
}
