using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public float ScoreMax;
    [SerializeField] Image PlayerImage;
    [SerializeField] Image EnemyImage;
    [SerializeField] float EnemyScore;
    [SerializeField] float PlayerScore;
    [SerializeField] TextMeshProUGUI EnemyScoreText;
    [SerializeField] TextMeshProUGUI PlayerScoreText;
    [SerializeField] TextMeshProUGUI ScoreText;

    [SerializeField] GameObject finishGame;
    [SerializeField] GameObject escenariGame;

    [SerializeField] GameObject winner;
    [SerializeField] GameObject lose;

    private void Start()
    {
        ScoreMax = Random.Range(50f, 150f);
        ScoreText.text = ScoreMax.ToString();
        Cursor.visible = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (EnemyScore < ScoreMax && PlayerScore < ScoreMax)
        {
            if (other.CompareTag("mlagent"))
            {
                EnemyScore += 0.05f;
            }
            if (other.CompareTag("Player"))
            {
                PlayerScore += 0.05f;
            }
        }
        else
        {
            Cursor.visible = true;
            finishGame.SetActive(true);
            if (PlayerScore > EnemyScore)
            {
                winner.SetActive(true);
            }
            else
            {
                lose.SetActive(true);
            }
            escenariGame.SetActive(false);
        }
    }
    private void Update()
    {
        PlayerImage.fillAmount = PlayerScore / ScoreMax;
        PlayerScoreText.text = PlayerScore.ToString();

        EnemyImage.fillAmount = EnemyScore / ScoreMax;
        EnemyScoreText.text = EnemyScore.ToString();

    }
}
