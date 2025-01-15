using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float CurrentHp { get; }
    float MaxHp { get; }
    bool IsDead { get; }
    void TakeDamage(float atk, bool isCritical);
    void Die();
}
