using UnityEngine;

public class TestPlayerSpawner : MythologyMayhem
{
    [Header("Overrides")]
    [SerializeField] bool overrideSpawn;
    [SerializeField] int spawnPointOverride;
    [SerializeField] bool overridePlayer;
    [SerializeField] int playerIndexOverride;

    [Header("Assign in Inspector")]
    [SerializeField] Dimension sceneDimension; //0 = 2D, 1 = 3D
    [SerializeField] Vector3[] spawnPoints; //Vector3 positions of spawn point game objects
    [SerializeField] playerSelectable[] _select = new playerSelectable[2]; //0 = 2D, 1 = 3D
    [SerializeField] CompanionController[] _ccs = new CompanionController[2]; //0 = 2D, 1 = 3D

    [Header("Assigned in Script")]
    [SerializeField] GameObject _player;

    private void OnEnable()
    {
        //Assign a 2D or 3D Character based on the scene dimension and the active player index
        int _index = -1;
        if(overridePlayer)
        {
            _index = playerIndexOverride;
        } else
        {
            _index = PlayerPrefs.GetInt("playerIndex");
        }
        _player = _select[((int)sceneDimension)].playerPrefabs[_index];

        //Spawn the _player as a child of TestPlayerSpawner at the spawn point location
        _index = -1;
        if(overrideSpawn)
        {
            _index = spawnPointOverride;
        } else
        {
            _index = PlayerPrefs.GetInt("spawnPointIndex");
        }
        _player = Instantiate(_player, spawnPoints[_index], Quaternion.identity, gameObject.transform);

        //Define the Companion Controller
        foreach(Companion _companion in _ccs[(int)sceneDimension].companions)
        {
            _companion._player = _player;
        }

        //Define Enemy properties
        Enemy[] _enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy _enemy in _enemies) 
        {
            _enemy.player = _player;
        }

    }//end on enable

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
