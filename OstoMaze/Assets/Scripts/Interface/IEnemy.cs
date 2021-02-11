using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {

    // Start is called before the first frame update
    void TakeDamage(float damage);

    Vector3 GetPosition();
}
