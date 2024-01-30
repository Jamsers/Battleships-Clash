using UnityEngine;
using UnityEngine.UI;

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
		playMenu.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(true);
		isGamePaused = 1;
		Time.timeScale = 0f;
		music.Pause();
	}

	private void ResumeGame()
	{
		pauseMenu.gameObject.SetActive(false);
		playMenu.gameObject.SetActive(true);
		isGamePaused = 0;
		Time.timeScale = 1f;
		music.Play();
		if (isFirstStart)
		{
			startButtonText.SetActive(false);
			resumeButtonText.SetActive(true);
		}
	}

	public void GameOver()
	{
		pauseMenu.gameObject.SetActive(false);
		playMenu.gameObject.SetActive(false);
		gameOverMenu.gameObject.SetActive(true);
		isGamePaused = 2;
		music.Pause();
		scoreText.text = score.ToString();
		spawnText.text = spawnScore.ToString();
		boatText.text = boatScore.ToString();
		subText.text = subScore.ToString();
		int @int = PlayerPrefs.GetInt("Score", 0);
		if (@int < score)
		{
			newHighScore.gameObject.SetActive(true);
			PlayerPrefs.SetInt("Score", score);
			PlayerPrefs.SetInt("Spawn Score", spawnScore);
			PlayerPrefs.SetInt("Boat Score", boatScore);
			PlayerPrefs.SetInt("Sub Score", subScore);
			PlayerPrefs.Save();
		}
	}

	private void ShowCredit()
	{
		pauseMenu.gameObject.SetActive(false);
		creditScreen.gameObject.SetActive(true);
	}

	private void ShowHighScore()
	{
		pauseMenu.gameObject.SetActive(false);
		highScoreScreen.gameObject.SetActive(true);
		spawnScoreText.text = PlayerPrefs.GetInt("Spawn Score", 0).ToString();
		boatScoreText.text = PlayerPrefs.GetInt("Boat Score", 0).ToString();
		subScoreText.text = PlayerPrefs.GetInt("Sub Score", 0).ToString();
		highScoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
	}

	private void BackToPause()
	{
		creditScreen.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(true);
	}

	private void BackToPauseFromHighScore()
	{
		highScoreScreen.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(true);
	}

	private void ExitGame()
	{
		Application.Quit();
	}

	private void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	private void PlayerMovement()
	{
		float num = ((isGamePaused != 2) ? Input.acceleration.x : 0f);
		Vector3 vector = new Vector3(num * leftRightMultiplier, 0f, upMovement);
		gameSpace.transform.position = gameSpace.transform.position + vector * Time.deltaTime;
	}

	private void PlayerShooting()
	{
		if (Input.GetButton("Fire1") && Time.time - shootTime > shootRate)
		{
			Vector3 vector = new Vector3(0f, missileYOffset, 0f);
			Object.Instantiate(missile, player.transform.position + vector, missile.transform.rotation);
			shootTime = Time.time;
		}
	}

	private void Spawner()
	{
		if (Time.time - spawnTime > spawnInterval)
		{
			spawnBoxLeftEdge = spawnBox.transform.position.x - spawnBox.transform.localScale.x * 0.5f;
			spawnBoxRightEdge = spawnBox.transform.position.x + spawnBox.transform.localScale.x * 0.5f;
			spawnBoxTopEdge = spawnBox.transform.position.z + spawnBox.transform.localScale.z * 0.5f;
			spawnBoxHeight = spawnBox.transform.position.y;
			float num = Random.Range(0, 100);
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
		float x = Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + islandSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 vector = new Vector3(x, y, z);
		float num = Random.Range(0, 3);
		if (num < 1f)
		{
			Object.Instantiate(island1, island1.transform.position + vector, island1.transform.rotation);
		}
		if (num < 2f)
		{
			Object.Instantiate(island2, island2.transform.position + vector, island2.transform.rotation);
		}
		if (num < 3f)
		{
			Object.Instantiate(island3, island3.transform.position + vector, island3.transform.rotation);
		}
	}

	private void SpawnAttackIsland()
	{
		float x = Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + islandSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 vector = new Vector3(x, y, z);
		Object.Instantiate(attackIsland, attackIsland.transform.position + vector, attackIsland.transform.rotation);
	}

	private void SpawnSub()
	{
		float x = Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + submarineSpawnDepth;
		float z = spawnBoxTopEdge;
		Vector3 vector = new Vector3(x, y, z);
		Object.Instantiate(submarine, submarine.transform.position + vector, submarine.transform.rotation);
	}

	private void SpawnFastSub()
	{
		float x = Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight + submarineSpawnDepth;
		float z = spawnBoxTopEdge;
		Object.Instantiate(position: new Vector3(x, y, z), original: fastSub, rotation: fastSub.transform.rotation);
	}

	private void SpawnBoat()
	{
		float x = Random.Range(spawnBoxLeftEdge, spawnBoxRightEdge);
		float y = spawnBoxHeight;
		float z = spawnBoxTopEdge;
		Vector3 vector = new Vector3(x, y, z);
		Object.Instantiate(boat, boat.transform.position + vector, boat.transform.rotation);
	}
}
