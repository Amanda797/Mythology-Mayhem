using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegirControlScript : MonoBehaviour
{
    public State curState;
    public int health;

    [Header("Spawning")]
    public float riseSpeed;
    public Transform risePosition;
    public float distanceToStart;

    [Header("Small Wave")]
    public int smallWaveDamage;
    public int curSmallWave;
    public float smallWaveCooldown1;
    public float smallWaveCooldown2;
    public float smallWaveCooldown3;
    public float lastSmall;
    public GameObject smallWavePrefab;
    public Transform waveSpawnPoint;

    [Header("Medium Wave")]
    public int mediuemWaveDamage;
    public GameObject mediumWavePrefab;
    public float mediumWaveCooldown;
    public float lastMed;

    [Header("Large Wave")]
    public int largeWaveDamage;
    public GameObject largeWavePrefab;
    public float largeWaveCooldown;
    public float lastLarge;

    [Header("Water Jet")]
    public int waterJetDamage;
    public bool jetStarted;
    public float jetStart;
    public float jetTime;
    public GameObject waterJet;

    [Header("Healing")]
    public float startKrakenTime;
    public float krakenSummonTime;
    public int healAmount;
    public float startHealTime;
    public float healTime;
    public int healthStart;
    public bool krakenActive;

    public bool startFight;

    public Animator anim;

    public enum State 
    { 
        Hidden,
        Spawn,
        SmallWaveAttack1,
        WaterJet1,
        Kraken1,
        Heal1,
        WaitForKraken1,
        SmallWaveAttack2,
        WaterJet2,
        MediumWaveAttack2,
        Kraken2,
        Heal2,
        WaitForKraken2,
        SmallWaveAttack3,
        WaterJet3,
        MediumWaveAttack3,
        Kraken3,
        Heal3,
        WaitForKraken3,
        SmallWaveAttack4,
        WaterJet4,
        LargeWaveAttack4,
        Kraken4,
        Heal4,
        WaitForKraken4,
        MediumWaveAttack5,
        WaterJet5,
        LargeWaveAttack5,
        Death

    }
    // Start is called before the first frame update
    void Start()
    {
        startFight = false;
        curSmallWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RunState();
    }

    void RunState() 
    {
        switch (curState) 
        {
            case State.Hidden:
                if (startFight) 
                {
                    ChangeState(State.Spawn);
                }
                break;
            case State.Spawn:
                transform.position = Vector3.MoveTowards(transform.position, risePosition.position, riseSpeed * Time.deltaTime);
                float riseDist = Vector3.Distance(transform.position, risePosition.position);
                if (riseDist <= distanceToStart) 
                {
                    transform.position = risePosition.position;
                    ChangeState(State.SmallWaveAttack1);
                }
                break;
            case State.SmallWaveAttack1:
                if (curSmallWave == 1) 
                {
                    WaveSmall();
                    lastSmall = Time.time;
                    curSmallWave = 2;
                }
                if (curSmallWave == 2) 
                {
                    if (smallWaveCooldown1 < (Time.time - lastSmall)) 
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 3;
                    }
                }
                if (curSmallWave == 3) 
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall)) 
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 4;
                    }
                }
                if (curSmallWave == 4) 
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaterJet();
                        lastSmall = Time.time;
                        curSmallWave = 0;
                        ChangeState(State.WaterJet1);
                    }
                }
                break;
            case State.WaterJet1:
                if (jetStarted) 
                {
                    if (jetTime <= (Time.time - jetStart)) 
                    {
                        anim.SetBool("Jet", false);
                        anim.SetTrigger("Kraken");
                        waterJet.SetActive(false);
                        jetStarted = false;
                        startKrakenTime = Time.time;
                        krakenActive = true;
                        ChangeState(State.Kraken1);
                    }
                }
                break;
            case State.Kraken1:
                if (krakenSummonTime < (Time.time - startKrakenTime))
                {
                    healthStart = health;
                    startHealTime = 0;
                    ChangeState(State.Heal1);
                }
                break;
            case State.Heal1:
                startHealTime += Time.deltaTime;
                if (startHealTime >= healTime) 
                {
                    startHealTime = healTime;
                }
                health = Mathf.RoundToInt(healthStart + (healAmount * (startHealTime / healTime)));
                if (startHealTime >= healTime) 
                {
                    ChangeState(State.WaitForKraken1);
                }
                break;
            case State.WaitForKraken1:
                if (!krakenActive) 
                {
                    ChangeState(State.SmallWaveAttack2);
                }
                break;
            case State.SmallWaveAttack2:
                if (curSmallWave == 1)
                {
                    WaveSmall();
                    lastSmall = Time.time;
                    curSmallWave = 2;
                }
                if (curSmallWave == 2)
                {
                    if (smallWaveCooldown1 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 3;
                    }
                }
                if (curSmallWave == 3)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 4;
                    }
                }
                if (curSmallWave == 4)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaterJet();
                        lastSmall = Time.time;
                        curSmallWave = 0;
                        ChangeState(State.WaterJet2);
                    }
                }
                break;
            case State.WaterJet2:
                if (jetStarted)
                {
                    if (jetTime <= (Time.time - jetStart))
                    {
                        anim.SetBool("Jet", false);
                        waterJet.SetActive(false);
                        jetStarted = false;
                        WaveMedium();
                        lastMed = Time.time;
                        ChangeState(State.MediumWaveAttack2);
                    }
                }
                break;
            case State.MediumWaveAttack2:
                if (mediumWaveCooldown <= (Time.time - lastMed)) 
                {
                    startKrakenTime = Time.time;
                    krakenActive = true;
                    anim.SetTrigger("Kraken");
                    ChangeState(State.Kraken2);
                }
                break;
            case State.Kraken2:
                if (krakenSummonTime < (Time.time - startKrakenTime))
                {
                    healthStart = health;
                    startHealTime = 0;
                    ChangeState(State.Heal2);
                }
                break;
            case State.Heal2:
                startHealTime += Time.deltaTime;
                if (startHealTime >= healTime)
                {
                    startHealTime = healTime;
                }
                health = Mathf.RoundToInt(healthStart + (healAmount * (startHealTime / healTime)));
                if (startHealTime >= healTime)
                {
                    ChangeState(State.WaitForKraken2);
                }
                break;
            case State.WaitForKraken2:
                if (!krakenActive)
                {
                    ChangeState(State.SmallWaveAttack3);
                }
                break;
            case State.SmallWaveAttack3:
                if (curSmallWave == 1)
                {
                    WaveSmall();
                    lastSmall = Time.time;
                    curSmallWave = 2;
                }
                if (curSmallWave == 2)
                {
                    if (smallWaveCooldown1 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 3;
                    }
                }
                if (curSmallWave == 3)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 4;
                    }
                }
                if (curSmallWave == 4)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaterJet();
                        lastSmall = Time.time;
                        curSmallWave = 0;
                        ChangeState(State.WaterJet3);
                    }
                }
                break;
            case State.WaterJet3:
                if (jetStarted)
                {
                    if (jetTime <= (Time.time - jetStart))
                    {
                        anim.SetBool("Jet", false);
                        waterJet.SetActive(false);
                        jetStarted = false;
                        WaveMedium();
                        lastMed = Time.time;
                        ChangeState(State.MediumWaveAttack3);
                    }
                }
                break;
            case State.MediumWaveAttack3:
                if (mediumWaveCooldown <= (Time.time - lastMed))
                {
                    startKrakenTime = Time.time;
                    krakenActive = true;
                    anim.SetTrigger("Kraken");
                    ChangeState(State.Kraken3);
                }
                break;
            case State.Kraken3:
                if (krakenSummonTime < (Time.time - startKrakenTime))
                {
                    healthStart = health;
                    startHealTime = 0;
                    ChangeState(State.Heal3);
                }
                break;
            case State.Heal3:
                startHealTime += Time.deltaTime;
                if (startHealTime >= healTime)
                {
                    startHealTime = healTime;
                }
                health = Mathf.RoundToInt(healthStart + (healAmount * (startHealTime / healTime)));
                if (startHealTime >= healTime)
                {
                    ChangeState(State.WaitForKraken3);
                }
                break;
            case State.WaitForKraken3:
                if (!krakenActive)
                {
                    ChangeState(State.SmallWaveAttack4);
                }
                break;
            case State.SmallWaveAttack4:
                if (curSmallWave == 1)
                {
                    WaveSmall();
                    lastSmall = Time.time;
                    curSmallWave = 2;
                }
                if (curSmallWave == 2)
                {
                    if (smallWaveCooldown1 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 3;
                    }
                }
                if (curSmallWave == 3)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaveSmall();
                        lastSmall = Time.time;
                        curSmallWave = 4;
                    }
                }
                if (curSmallWave == 4)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaterJet();
                        lastSmall = Time.time;
                        curSmallWave = 0;
                        ChangeState(State.WaterJet4);
                    }
                }
                break;
            case State.WaterJet4:
                if (jetStarted)
                {
                    if (jetTime <= (Time.time - jetStart))
                    {
                        anim.SetBool("Jet", false);
                        waterJet.SetActive(false);
                        jetStarted = false;
                        WaveLarge();
                        lastLarge = Time.time;
                        ChangeState(State.LargeWaveAttack4);
                    }
                }
                break;
            case State.LargeWaveAttack4:
                if (largeWaveCooldown <= (Time.time - lastLarge))
                {
                    startKrakenTime = Time.time;
                    krakenActive = true;
                    anim.SetTrigger("Kraken");
                    ChangeState(State.Kraken4);
                }
                break;
            case State.Kraken4:
                if (krakenSummonTime < (Time.time - startKrakenTime))
                {
                    healthStart = health;
                    startHealTime = 0;
                    ChangeState(State.Heal4);
                }
                break;
            case State.Heal4:
                startHealTime += Time.deltaTime;
                if (startHealTime >= healTime)
                {
                    startHealTime = healTime;
                }
                health = Mathf.RoundToInt(healthStart + (healAmount * (startHealTime / healTime)));
                if (startHealTime >= healTime)
                {
                    ChangeState(State.WaitForKraken4);
                }
                break;
            case State.WaitForKraken4:
                if (!krakenActive)
                {
                    ChangeState(State.MediumWaveAttack5);
                }
                break;
            case State.MediumWaveAttack5:
                if (curSmallWave == 1)
                {
                    WaveMedium();
                    lastSmall = Time.time;
                    curSmallWave = 2;
                }
                if (curSmallWave == 2)
                {
                    if (smallWaveCooldown1 < (Time.time - lastSmall))
                    {
                        WaveMedium();
                        lastSmall = Time.time;
                        curSmallWave = 3;
                    }
                }
                if (curSmallWave == 3)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaveMedium();
                        lastSmall = Time.time;
                        curSmallWave = 4;
                    }
                }
                if (curSmallWave == 4)
                {
                    if (smallWaveCooldown2 < (Time.time - lastSmall))
                    {
                        WaterJet();
                        lastSmall = Time.time;
                        curSmallWave = 0;
                        ChangeState(State.WaterJet5);
                    }
                }
                break;
            case State.WaterJet5:
                if (jetStarted)
                {
                    if (jetTime <= (Time.time - jetStart))
                    {
                        anim.SetBool("Jet", false);
                        waterJet.SetActive(false);
                        jetStarted = false;
                        WaveLarge();
                        lastLarge = Time.time;
                        ChangeState(State.LargeWaveAttack4);
                    }
                }
                break;
            case State.LargeWaveAttack5:
                if (largeWaveCooldown <= (Time.time - lastLarge))
                {
                    ChangeState(State.MediumWaveAttack5);
                }
                break;
            case State.Death:

                break;
        }
        
    }


    void ChangeState(State state)
    {
        curState = state;
        switch (state)
        {
            case State.Hidden:

                return;
            case State.Spawn:

                return;
            case State.SmallWaveAttack1:
                curSmallWave = 1;
                lastSmall = Time.time;
                return;
            case State.WaterJet1:

                return;
            case State.Kraken1:

                return;
            case State.Heal1:

                return;
            case State.WaitForKraken1:

                return;
            case State.SmallWaveAttack2:
                curSmallWave = 1;
                lastSmall = Time.time;
                return;
            case State.WaterJet2:

                return;
            case State.MediumWaveAttack2:

                return;
            case State.Kraken2:

                return;
            case State.Heal2:

                return;
            case State.SmallWaveAttack3:
                curSmallWave = 1;
                lastSmall = Time.time;
                return;
            case State.WaterJet3:

                return;
            case State.MediumWaveAttack3:

                return;
            case State.Kraken3:

                return;
            case State.Heal3:

                return;
            case State.SmallWaveAttack4:
                curSmallWave = 1;
                lastSmall = Time.time;
                return;
            case State.WaterJet4:

                return;
            case State.LargeWaveAttack4:

                return;
            case State.Kraken4:

                return;
            case State.Heal4:

                return;
            case State.MediumWaveAttack5:
                curSmallWave = 1;
                lastSmall = Time.time;
                return;
            case State.WaterJet5:

                return;
            case State.LargeWaveAttack5:

                return;
            case State.Death:

                return;
        }
    }

    void WaveSmall() 
    {
        int which = (int)Random.Range(1, 2);
        if (which == 1)
        {
            anim.SetTrigger("Wave1");
        }
        else 
        {
            anim.SetTrigger("Wave2");
        }

        GameObject obj = Instantiate(smallWavePrefab, waveSpawnPoint.position, waveSpawnPoint.rotation);
        AegirWaveScript waveScript = obj.GetComponent<AegirWaveScript>();
        if (waveScript != null) 
        {
            waveScript.damage = smallWaveDamage;
        }
    }

    void WaveMedium() 
    {
        anim.SetTrigger("Wave3");
        GameObject obj = Instantiate(mediumWavePrefab, waveSpawnPoint.position, waveSpawnPoint.rotation);
        AegirWaveScript waveScript = obj.GetComponent<AegirWaveScript>();
        if (waveScript != null)
        {
            waveScript.damage = mediuemWaveDamage;
        }
    }

    void WaveLarge() 
    {
        anim.SetTrigger("Wave4");
        GameObject obj = Instantiate(largeWavePrefab, waveSpawnPoint.position, waveSpawnPoint.rotation);
        AegirWaveScript waveScript = obj.GetComponent<AegirWaveScript>();
        if (waveScript != null)
        {
            waveScript.damage = largeWaveDamage;
        }
    }

    void WaterJet() 
    {
        anim.SetBool("Jet", true);
    }

    public void ActivateJet() 
    {
        jetStarted = true;
        jetStart = Time.time;
        waterJet.SetActive(true);
    }
    void SummonKraken() 
    { 
    
    }

    void Heal() 
    { 
    
    }
}
