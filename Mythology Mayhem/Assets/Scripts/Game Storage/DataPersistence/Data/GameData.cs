using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public float playerHealth;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public SerializableDictionary<int, Vector3> enemyLocation;
    public SerializableDictionary<int, Quaternion> enemyRotation;
    public SerializableDictionary<int, int> enemyWaypoint;
    public SerializableDictionary<int, float> enemyHealth;
    public SerializableDictionary<string, bool> keys;
    public SerializableDictionary<string, bool> artifacts;
    public SerializableDictionary<string, bool> scrolls;
    public SerializableDictionary<string, bool> potions;
    public int abilityCount;
    public int weaponCount;
    public string currentSceneName;
    public int currentSceneBuildIndex;
    public float masterVolume;
    public float musicVolume;
    public float effectVolume;

    public GameData()
    {
        playerHealth = 10;
        playerPosition = new Vector3();
        playerRotation = new Quaternion(0f, 173.439f, 0f, 0f);
        enemyLocation = new SerializableDictionary<int, Vector3>();
        enemyRotation = new SerializableDictionary<int, Quaternion>();
        enemyWaypoint = new SerializableDictionary<int, int>();
        enemyHealth = new SerializableDictionary<int, float>();
        keys = new SerializableDictionary<string, bool>();
        artifacts = new SerializableDictionary<string, bool>();
        scrolls = new SerializableDictionary<string, bool>();
        potions = new SerializableDictionary<string, bool>();
        abilityCount = 0;
        weaponCount = 0;
        currentSceneName = "Library of Alexandiria";
        currentSceneBuildIndex = 0;
        masterVolume = 1;
        musicVolume = 1;
        effectVolume = 1;
    }
}
