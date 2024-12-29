using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public Text scoreText; // 점수 텍스트 UI
    public Text timeText;  // 시간 텍스트 UI
    public float gameTime = 0f; // 게임 시간
    private int score = 0; // 현재 점수

    void Update()
    {
        // 시간 업데이트
        gameTime += Time.deltaTime;
        UpdateTimeUI();
    }

    // 점수 업데이트 함수
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // 점수 UI 갱신
    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    // 시간 UI 갱신
    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
    }
}
