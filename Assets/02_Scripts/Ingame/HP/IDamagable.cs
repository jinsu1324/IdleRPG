using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float CurrentHp { get; }
    float MaxHp { get; }
    bool IsDead { get; }
    void TakeDamage(TakeDamageArgs args);
    void Die();
}
