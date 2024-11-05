using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEngine : MonoBehaviour
{
	public int isGamePaused;

	private AudioSource music;

	public bool isFirstStart;

	public GameObject attackIsland;

	public GameObject fastSub;

	public GameObject boat;

	public float attackIslandPercentSpawn;

	public float islandPercentSpawn;

	public float fastSubPercentSpawn;

	public float subPercentSpawn;

	public float boatPercentSpawn;

	public Text scoreDisplay;

	public int score;

	public int spawnScore;

	public int boatScore;

	public int subScore;

	public GameObject creditScreen;

    public GameObject helpScreen;

    public GameObject pauseMenu;

	public GameObject playMenu;

	public GameObject gameOverMenu;

	public GameObject highScoreScreen;

	public Text highScoreText;

	public Text spawnScoreText;

	public Text boatScoreText;

	public Text subScoreText;

	public GameObject startButtonText;

	public GameObject resumeButtonText;

	public Text scoreText;

	public Text spawnText;

	public Text subText;

	public Text boatText;

	public float leftRightMultiplier;

	public float upMovement;

	public GameObject gameSpace;

	private float shootTime;

	public GameObject missile;

	public float shootRate;

	public float missileYOffset;

	public GameObject player;

	private float spawnTime;

	private float spawnBoxLeftEdge;

	private float spawnBoxRightEdge;

	private float spawnBoxTopEdge;

	private float spawnBoxHeight;

	public GameObject spawnBox;

	public float spawnInterval;

	public GameObject submarine;

	public float submarineSpawnDepth;

	public GameObject island1;

	public GameObject island2;

	public GameObject island3;

	public float islandSpawnDepth;

	private float scoreRate = 1f;

	private float scoreTime;

	public GameObject newHighScore;

	private void Start()
	{
		SetFullscreen(Screen.fullScreen);
        shootTime = Time.time;
		spawnTime = Time.time;
		scoreTime = Time.time;
		music = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (isGamePaused == 2)
		{
			Spawner();
		}
		if (isGamePaused == 0)
		{
			PlayerMovement();
			PlayerShooting();
			Spawner();
			if (Time.time - scoreTime > scoreRate)
			{
				AddToScore(0);
				scoreTime = Time.time;
			}
		}
	}

	public void SetFullscreen(bool isFullscreen)
	{
		if (isFullscreen && Screen.fullScreen == false)
            StartCoroutine("SetFullScreen");
		else if (!isFullscreen && Screen.fullScreen == true)
            Screen.SetResolution(1024, 768, false);
    }

	public void AddToScore(int enemyType)
	{
		if (enemyType == 0)
		{
			score++;
			spawnScore++;
			scoreDisplay.text = (int.Parse(scoreDisplay.text) + 1).ToString();
		}
		if (enemyType == 1)
		{
			score += 10;
			boatScore++;
			scoreDisplay.text = (int.Parse(scoreDisplay.text) + 10).ToString();
		}
		if (enemyType == 2)
		{
			score += 50;
			subScore++;
			scoreDisplay.text = (int.Parse(scoreDisplay.text) + 50).ToString();
		}
	}

	private void PauseGame()
	{
		playMenu.gameObject.SetActive(value: false);
		pauseMenu.gameObject.SetActive(value: true);
		isGamePaused = 1;
		Time.timeScale = 0f;
		music.Pause();
	}

	private void ResumeGame()
	{
		pauseMenu.gameObject.SetActive(value: false);
		playMenu.gameObject.SetActive(value: true);
		isGamePaused = 0;
		Time.timeScale = 1f;
		music.Play();
		if (isFirstStart)
		{
			startButtonText.SetActive(value: false);
			resumeButtonText.SetActive(value: true);
		}
	}

	public void GameOver()
	{
		pauseMenu.gameObject.SetActive(value: false);
		playMenu.gameObject.SetActive(value: false);
		gameOverMenu.gameObject.SetActive(value: true);
		isGamePaused = 2;
		music.Pause();
		scoreText.text = score.ToString();
		spawnText.text = spawnScore.ToString();
		boatText.text = boatScore.ToString();
		subText.text = subScore.ToString();
		int @int = PlayerPrefs.GetInt("Score", 0);
		if (@int < score)
		{
			newHighScore.gameObject.SetActive(value: true);
			PlayerPrefs.SetInt("Score", score);
			PlayerPrefs.SetInt("Spawn Score", spawnScore);
			PlayerPrefs.SetInt("Boat Score", boatScore);
			PlayerPrefs.SetInt("Sub Score", subScore);
			PlayerPrefs.Save();
		}
	}

	private void ShowCredit()
	{
		pauseMenu.gameObject.SetActive(value: false);
		creditScreen.gameObject.SetActive(value: true);
	}

    private void HelpScreen()
    {
        pauseMenu.gameObject.SetActive(value: false);
        helpScreen.gameObject.SetActive(value: true);
    }

    private void ToggleFullscreen()
    {
        if (Screen.fullScreen == false)
            SetFullscreen(true);
        else
            SetFullscreen(false);
    }

    private void ShowHighScore()
	{
		pauseMenu.gameObject.SetActive(value: false);
		highScoreScreen.gameObject.SetActive(value: true);
		spawnScoreText.text = PlayerPrefs.GetInt("Spawn Score", 0).ToString();
		boatScoreText.text = PlayerPrefs.GetInt("Boat Score", 0).ToString();
		subScoreText.text = PlayerPrefs.GetInt("Sub Score", 0).ToString();
		highScoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
	}

	private void BackToPause()
	{
		creditScreen.gameObject.SetActive(value: false);
        helpScreen.gameObject.SetActive(value: false);
        pauseMenu.gameObject.SetActive(value: true);
	}

	private void BackToPauseFromHighScore()
	{
		highScoreScreen.gameObject.SetActive(value: false);
		pauseMenu.gameObject.SetActive(value: true);
	}

	private void ExitGame()
	{
		Application.Quit();
	}

	private void RestartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	private void PlayerMovement()
	{
		float num;
		if (isGamePaused == 2)
		{
			num = 0f;
		}
		else
		{
			Vector3 acceleration = Input.acceleration;
			num = acceleration.x;

            // keyboard controls
            float horizontalAxis = Input.GetAxis("Horizontal");
			horizontalAxis = horizontalAxis / 2.5f;
            num = horizontalAxis;
        }
		Vector3 a = new Vector3(num * leftRightMultiplier, 0f, upMovement);
		gameSpace.transform.position = gameSpace.transform.position + a * Time.deltaTime;
	}

	private void PlayerShooting()
	{
		if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time - shootTime > shootRate)
		{
			Vector3 b = new Vector3(0f, missileYOffset, 0f);
			Object.Instantiate(missile, player.transform.position + b, missile.transform.rotation);
			shootTime = Time.time;
		}
	}

	private void Spawner()
	{
		if (Time.time - spawnTime > spawnInterval)
		{
			Vector3 position = spawnBox.transform.position;
			float x = position.x;
			Vector3 localScale = spawnBox.transform.localScale;
			spawnBoxLeftEdge = x - localScale.x * 0.5f;
			Vector3 position2 = spawnBox.transform.position;
			float x2 = position2.x;
			Vector3 localScale2 = spawnBox.transform.localScale;
			spawnBoxRightEdge = x2 + localScale2.x * 0.5f;
			Vector3 position3 = spawnBox.transform.position;
			float z = position3.z;
			Vector3 localScale3 = spawnBox.transform.localScale;
			spawnBoxTopEdge = z + localScale3.z * 1.0f;
			Vector3 position4 = spawnBox.transform.position;
			spawnBoxHeight = position4.y;
			float num = UnityEngine.Random.Range(0, 100);
			if (num < attackIslandPercentSpawn && isGamePaused == 0)
			{
				SpawnAttackIsland();
			}
			if (num < islandPercentSpawn && isGamePaused == 0)
			{
				SpawnIsland();
			}
			if (num < fastSubPercentSpawn)
			{
				SpawnFastSub();
			}
			if (num < subPercentSpawn)
			{
				SpawnSub();
			}
			if (num < boatPercentSpawn)
			{
				SpawnBoat();
			}
			spawnTime = Time.time;
		}
	}

	private void SpawnIsland()
	{
		float x = UnityEngine.Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + islandSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 b = new Vector3(x, y, z);
		float num = UnityEngine.Random.Range(0, 3);
		if (num < 1f)
		{
			Object.Instantiate(island1, island1.transform.position + b, island1.transform.rotation);
		}
		if (num < 2f)
		{
			Object.Instantiate(island2, island2.transform.position + b, island2.transform.rotation);
		}
		if (num < 3f)
		{
			Object.Instantiate(island3, island3.transform.position + b, island3.transform.rotation);
		}
	}

	private void SpawnAttackIsland()
	{
		float x = UnityEngine.Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + islandSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 b = new Vector3(x, y, z);
		Object.Instantiate(attackIsland, attackIsland.transform.position + b, attackIsland.transform.rotation);
	}

	private void SpawnSub()
	{
		float x = UnityEngine.Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + submarineSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 b = new Vector3(x, y, z);
		Object.Instantiate(submarine, submarine.transform.position + b, submarine.transform.rotation);
	}

	private void SpawnFastSub()
	{
		float x = UnityEngine.Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + submarineSpawnDepth;
		float z = spawnBoxTopEdge;
		Object.Instantiate(position: new Vector3(x, y, z), original: fastSub, rotation: fastSub.transform.rotation);
	}

	private void SpawnBoat()
	{
		float x = UnityEngine.Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight;
		float z = spawnBoxTopEdge;
		Vector3 b = new Vector3(x, y, z);
		Object.Instantiate(boat, boat.transform.position + b, boat.transform.rotation);
	}

    IEnumerator SetFullScreen()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);

        yield return new WaitForSeconds(0.01f);

        int interval = 3;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        float lerp = (float)interval / 3f;
        float origwidth = Mathf.Lerp(522 * ratio, Screen.currentResolution.width, lerp);
        float origheight = Mathf.Lerp(522, Screen.currentResolution.height, lerp);
        int width = Mathf.CeilToInt(origwidth);
        int height = Mathf.CeilToInt(origheight);
        Screen.SetResolution(width, height, true);
    }
}