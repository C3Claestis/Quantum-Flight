using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerStatus player;
    [SerializeField] RectTransform controlMove;
    [SerializeField] Lever2D controlLever;
    [SerializeField] RandomSpawnMeteorit randomSpawnMeteorit;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject panel_Resume;
    [HideInInspector] public bool isFuelFull;
    float speed = 0.03f;
    int UpDown, RigthLeft;
    int score;
    int scoreMultiplier;

    void Start()
    {
        UpDown = 1;
        RigthLeft = 1;
        scoreMultiplier = 1;  // Inisialisasi multiplier
    }

    void Update()
    {
        ClampPlayerPosition();

        ManageFuelAndSpeed();

        UpdateScore();

        ScoreText.text = score.ToString();
    }

    void FixedUpdate()
    {
        MovePlayerUpDown(UpDown);
        MovePlayerRightLeft(RigthLeft);
    }

    void ClampPlayerPosition()
    {
        Vector2 clampedPosition = player.transform.position;
        clampedPosition.x = Mathf.Clamp(player.transform.position.x, -7f, 7f);
        clampedPosition.y = Mathf.Clamp(player.transform.position.y, -5f, 5f);
        player.transform.position = clampedPosition;
    }

    void ManageFuelAndSpeed()
    {
        if (isFuelFull)
        {
            if (controlMove.anchoredPosition.y > 0)
            {
                SetPlayerConditions(true, 2, 5f, 1, 7, 200, 3);
            }
            else if (controlMove.anchoredPosition.y == -110)
            {
                SetPlayerConditions(false, 0, 1f, 3, 3, 75, 1);
            }
            else if (controlMove.anchoredPosition.y <= 0)
            {
                SetPlayerConditions(true, 1, 3f, 2, 5, 100, 2);
            }
        }
        else
        {
            SetPlayerConditions(false, 0, 1f, 3, 3, 75, 1);
            controlLever.MoveToBottomPosition();
        }
    }

    void SetPlayerConditions(bool fuelConds, int fuelCost, float playerSpeed, float spawnInterval, float spaceObjectSpeed, float rotationSpeed, int scores)
    {
        player.FuelConds = fuelConds;
        player.FuelCost = fuelCost;
        speed = playerSpeed;
        randomSpawnMeteorit.spawnInterval = spawnInterval;
        scoreMultiplier = scores;  // Set multiplier berdasarkan kondisi

        SpaceObject[] spaceObjects = FindObjectsOfType<SpaceObject>();
        foreach (SpaceObject spaceObject in spaceObjects)
        {
            spaceObject.Speed = spaceObjectSpeed;
            spaceObject.rotationSpeed = rotationSpeed;
        }
    }

    void UpdateScore()
    {
        score += (int)(Time.deltaTime * scoreMultiplier * 100);  // Tambah skor berdasarkan waktu dan multiplier
    }

    public void MovePlayerUpDown(int playerId)
    {
        switch (playerId)
        {
            case 0:
                UpDown = 0;
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - speed * Time.deltaTime);
                break;
            case 1:
                UpDown = 1;
                player.transform.position = player.transform.position;
                break;
            case 2:
                UpDown = 2;
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + speed * Time.deltaTime);
                break;
        }
    }

    public void MovePlayerRightLeft(int playerId)
    {
        switch (playerId)
        {
            case 0:
                RigthLeft = 0;
                player.transform.position = new Vector2(player.transform.position.x - speed * Time.deltaTime, player.transform.position.y);
                break;
            case 1:
                RigthLeft = 1;
                player.transform.position = player.transform.position;
                break;
            case 2:
                RigthLeft = 2;
                player.transform.position = new Vector2(player.transform.position.x + speed * Time.deltaTime, player.transform.position.y);
                break;
        }
    }

    public void Resume()
    {
        panel_Resume.SetActive(true);
        Time.timeScale = 0;
    }
    public void BackGame()
    {
        panel_Resume.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
