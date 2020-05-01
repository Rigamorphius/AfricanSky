using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveLives
{
    int Lives { get; set; }

    event EventHandler OnLivesChanged;
}
