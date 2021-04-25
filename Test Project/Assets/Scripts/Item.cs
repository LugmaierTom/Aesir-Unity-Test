using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour, IClickable
{
    private GameObject _gameObject;

    public int _id { get; set; }
    public int _collumn { get; set; }
    public bool _isSelected { get; set; }

    public delegate void DestroyHandler(int collumn);
    public event DestroyHandler Destroyed;

    private void Awake()
    {
        _gameObject = this.gameObject;
    }

    public void DestroyItem()
    {
        Destroyed?.Invoke(_collumn);
    }
}
