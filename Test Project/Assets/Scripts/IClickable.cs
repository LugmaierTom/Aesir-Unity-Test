using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IClickable
{
    int _id { get; set; }
    int _collumn { get; set; }
    bool _isSelected { get; set; }

    void SetItemProperties(int id, int collumn);
}
