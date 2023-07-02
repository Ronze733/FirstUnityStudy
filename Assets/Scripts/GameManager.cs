using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image heartImagePrefab;

    [SerializeField]
    private Transform hpPanel;

    private List<Image> heartImages = new List<Image>();

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameClearPanel;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void IncreaseCoin(int coin)
    {
        string coinS = coin.ToString();
        if (coin > 999)
        {
            coinS = "999+";
        }
        text.SetText(coinS);
    }

    public void ShowHp(int userHp)
    {
        foreach(Image heartImage in heartImages){
            Destroy(heartImage.gameObject);
        }
        heartImages.Clear();

        for(int i = 0; i < userHp; i++)
        {
            Image heartImage = Instantiate(heartImagePrefab, hpPanel);
            RectTransform heartTransform = heartImage.GetComponent<RectTransform>();
            float xPos = -160 + (i * 80);
            heartTransform.anchoredPosition = new Vector2(xPos, -7.6f);
            heartImages.Add(heartImage);
        }
    }

    public void SetGameOver()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        Background[] backgrounds = FindObjectsOfType<Background>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemySpawner != null)
        {
            enemySpawner.stopEnemyRoutine();
            if(enemies != null && backgrounds != null) {
                for(int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].setMoveSpeed(0);
                }
                backgrounds[0].setMoveSpeed(0);
                backgrounds[1].setMoveSpeed(0);
            }
        }

        ShowGameResultPanel(1);
    }

    public bool SetGameClear()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemySpawner != null)
        {
            enemySpawner.stopEnemyRoutine();
            if (enemies != null)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i].gameObject);
                }
            }
        }

        ShowGameResultPanel(2);

        return false;
    }

    void ShowGameResultPanel(int result)
    {
        if (result == 1)
            gameOverPanel.SetActive(true);
        else
            gameClearPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
