using UnityEngine;
using TMPro;
using System.Collections;

public class EndAfterXMoves : MonoBehaviour, IEndCondition
{
    [SerializeField] private int _movesLeft;
    [SerializeField] TextMeshProUGUI _movesLeftText;


    private void Start()
    {
        _movesLeftText.text = $"Moves left: {_movesLeft}";
    }

    public bool EndGame()
    {
        _movesLeft--;
        _movesLeftText.text = $"Moves left: {_movesLeft}";
        
        if (_movesLeft <= 0)
        {
            return true;
        }

        return false;
    }


    public int GetMovesLeft()
    {
        return _movesLeft;
    }
}
