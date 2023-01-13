using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthController
{
    public void Kill() { }
    
    public void DealDamage(float value) { }
}
