using UnityEngine;
using System.Collections.Generic;
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


    void Start()
    {
        _selectionManager._MoveDone += UpdateScore;
        _scoreText.text = "Score: 0";
    }


    private void UpdateScore(List<GameObject> items)
    {
        _scoreCount += (items.Count * 10);
        _scoreText.text = $"Score: {_scoreCount}";
    }
}
