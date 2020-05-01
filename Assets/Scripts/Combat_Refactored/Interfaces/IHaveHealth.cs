using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    int Health { get; set; }

    event EventHandler OnHealthChanged;
}
