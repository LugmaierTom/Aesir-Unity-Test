using UnityEngine;
using System.Collections;

public class EndAfterXMoves : MonoBehaviour, IEndCondition
{
    [SerializeField] private int _movesTillEnd;

    private void Awake()
    {
        Debug.Log(_movesTillEnd);
    }

    public void EndGame()
    {
        _movesTillEnd--;
        
        if (_movesTillEnd <= 0)
        {
            Application.Quit();
        }
    }
}
