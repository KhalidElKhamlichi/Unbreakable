using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FiringStrategy {
    void shoot(GameObject projectile, Transform emissionPoint);
}
