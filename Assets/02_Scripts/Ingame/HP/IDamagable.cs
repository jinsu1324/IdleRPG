using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int CurrentHp { get; }
    int MaxHp { get; }
    bool IsDead { get; }
    void TakeDamage(int atk, bool isCritical);
    void Die();
}
