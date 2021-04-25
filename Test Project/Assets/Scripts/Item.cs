using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour, IClickable
{
    public int _id { get; set; }
    public int _collumn { get; set; }
    public bool _isSelected { get; set; }
    

    private void Awake()
    {
        _isSelected = false;
    }


    public void SetItemProperties(int id, int collumn)
    {
        _id = id;
        _collumn = collumn;
    }
}
