using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour, IClickable
{
    private GameObject _gameObject;

    public int _collumn { get; set; }
    public bool _isSelected { get; set; }

    private void Awake()
    {
        _gameObject = this.gameObject;
    }

    public void DestroyItem()
    {
        //Destory
    }

    public void SetCollumn(int collumn)
    {
        _collumn = collumn;
    }
}
