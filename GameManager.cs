using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    next, play, gameover, win, pause
};

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private Text moneyLBL;
    [SerializeField]
    private Text currentWaveLBL;
    [SerializeField]
    private Text playButtonLBL;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Text totalEscapedLBL;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private Enemy[] enemies;
    [SerializeField]
    private int totalEnemies = 3;
    [SerializeField]
    private int enemiesPerSpawn;
    const float spawnDelay = 0.5f;
    public List<Enemy> EnemyList = new List<Enemy>();
    private int totalMoney = 0;
    private int whichEnemiesToSpawn = 0;
    private int enemiesToSpawn = 0;

    public int TotalEscaped { get; set; } = 0;

    public int RoundEscaped { get; set; } = 0;

    public int TotalKilled { get; set; } = 0;

    public int WaveNumber { get; set; } = 0;

    public GameStatus CurrentState { get; private set; } = GameStatus.play;

    public AudioSource AudioSource { get; private set; }

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            moneyLBL.text = TotalMoney.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playButton.gameObject.SetActive(false);
        AudioSource = GetComponent<AudioSource>();
        showMenu();
    }

    private void Update()
    {
        handleEscape();
    }

    //Spawns enemies
    void spawnEnemy() {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies) {
            for (int i = 0; i < enemiesPerSpawn; i++) { //Creates enemies while the enemies on screen is less than max
                if (EnemyList.Count < totalEnemies) {
                    Enemy newEnemy = Instantiate(enemies[0]) as Enemy; //Cast as a game object
                    newEnemy.transform.position = spawnPoint.transform.position; //Creates the enmey and places it at the spawn point we created
                }
            }
        }
    }

    IEnumerator spawn() {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            { //Creates enemies while the enemies on screen is less than max
                if (EnemyList.Count < totalEnemies)
                {
                    Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy; //Cast as a game object
                    newEnemy.transform.position = spawnPoint.transform.position; //Creates the enmey and places it at the spawn point we created
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        IsWaveOver();
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    public void IsWaveOver()
    {
        totalEscapedLBL.text = "Escaped " + TotalEscaped + "/10";
        if ((RoundEscaped + TotalKilled) == totalEnemies)
        {
            if (WaveNumber <= enemies.Length)
            {
                enemiesToSpawn = WaveNumber;
            }
            setCurrentGameFunction();
            showMenu();
        }
    }

    public void setCurrentGameFunction()
    {
        if (TotalEscaped >= 10)
        {
            CurrentState = GameStatus.gameover;
        }
        else if (WaveNumber == 0 && (TotalKilled + RoundEscaped) == 0)
        {
            CurrentState = GameStatus.play;
        }
        else if (WaveNumber >= totalWaves)
        {
            CurrentState = GameStatus.win;
        }
        else
        {
            CurrentState = GameStatus.next;
        }
    }

    public void showMenu()
    {
        switch (CurrentState)
        {
            case GameStatus.play:
                playButtonLBL.text = "Play";
                break;
            case GameStatus.gameover:
                playButtonLBL.text = "Play Again";
                AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
                break;
            case GameStatus.next:
                playButtonLBL.text = "Next Wave";
                break;
            case GameStatus.pause:
                playButtonLBL.text = "Pause";
                break;
            case GameStatus.win:
                playButtonLBL.text = "Play";
                break;
        }
        playButton.gameObject.SetActive(true);
    }

    public void playButtonPressed()
    {
        switch (CurrentState)
        {
            case GameStatus.next:
                WaveNumber += 1;
                totalEnemies += WaveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                WaveNumber = 0;
                //enemiesToSpawn = 0;
                TotalMoney = 10;
                TowerManager.Instance.DestroyTowers();
                TowerManager.Instance.RenameBuildSites();
                moneyLBL.text = TotalMoney.ToString();
                enemiesToSpawn = 0;
                totalEscapedLBL.text = "Escaped " + TotalEscaped + "/10";
                AudioSource.PlayOneShot(SoundManager.Instance.NewGame);
                break;
        }
        DestroyAllEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWaveLBL.text = "Wave " + (WaveNumber + 1);
        StartCoroutine(spawn());
        playButton.gameObject.SetActive(false);
    }

    private void handleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TowerManager.Instance.disableDragSprite();
            TowerManager.Instance.towerButtonPressed = null;
        }
    }
}