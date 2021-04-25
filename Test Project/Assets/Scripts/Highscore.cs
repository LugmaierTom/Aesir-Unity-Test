using UnityEngine;
using System.Collections;
using TMPro;

public class Highscore : MonoBehaviour
{
    [SerializeField] private SelectionManager _selectionManager;
    private TextMeshProUGUI _scoreText;
    private int _scoreCount;

    private void Awake()
    {
        _scoreText = this.GetComponent<TextMeshProUGUI>();
    }

    // Use this for initialization
    void Start()
    {
        _selectionManager._MoveDone += UpdateScore;
        _scoreText.text = "Score: 0";
    }

    private void UpdateScore(int score)
    {
        _scoreCount += score;
        _scoreText.text = $"Score: {_scoreCount}";
    }
}
