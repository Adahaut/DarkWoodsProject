using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ParticleSystemCollision : MonoBehaviour
{
    public DW_Class ownerClass;
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 31)
        {
            // modifier le layer avec le futur layer officiel
            // retirer ici la vie de l'ennemi.

            float basePercent = Random.Range(ownerClass.minPercentDamage, ownerClass.maxPercentDamage);
            float baseDmg = Random.Range(ownerClass.minattackDamage, ownerClass.maxattackDamage);
            baseDmg += (basePercent / 100) * baseDmg;
            baseDmg /= 50;

            print(baseDmg);
        }
    }
}
