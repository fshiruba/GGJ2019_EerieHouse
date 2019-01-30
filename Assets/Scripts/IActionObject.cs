using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IActionObject
{
    void Test();
    bool IsHaunted { get; set; }
    Color DefaultColor { get; set; }
    void Haunt();
    void StopHaunt();
}

