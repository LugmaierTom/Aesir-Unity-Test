﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IClickable
{
    int _collumn { get; set; }
    bool _isSelected { get; set; }
    
    void DestroyItem();
}