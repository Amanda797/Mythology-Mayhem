using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorStats : MonoBehaviour
{

    public bool mirrorCoolDown;

    public float slowingValue;

    public float enemySlowDuration = 5f;
    public float coolDownDuration = 5f;

    public float slowingRadius = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("mirrorBool") == 1)
        {
            if(Input.GetKeyDown(KeyCode.Alpha3) && mirrorCoolDown == false)
            {
                SetEnemySpeed(); 
                mirrorCoolDown = true;
                Invoke("ResetEnemySpeed", enemySlowDuration);
                Invoke("ResetMirror", coolDownDuration);
            }
        }

        
    }

    private void SetEnemySpeed()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        MouseAI[] enemies = FindObjectsOfType<MouseAI>();

        foreach(MouseAI enemy in enemies)
        {
            if(Vector3.Distance(player.transform.position, enemy.transform.position) < slowingRadius)
            {
                enemy.SetMovementSpeed(slowingValue);
            }
        }

    }

    private void ResetMirror() 
    {
        mirrorCoolDown = false;


    }

    private void ResetEnemySpeed()
    {
         MouseAI[] enemies = FindObjectsOfType<MouseAI>();

        foreach(MouseAI enemy in enemies)
        {
            enemy.ResetMovementSpeed();
        }

    }

}
