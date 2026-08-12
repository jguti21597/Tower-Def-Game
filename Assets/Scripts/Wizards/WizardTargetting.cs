using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public enum TargetType
    {
        First,
        Last,
        CLose
    }

    public static Enemy GetTarget(WizardActions CurrentWizard, TargetType TargetMethod)
    {
        Collider[] EnemiesInRange = Physics.OverlapSphere(CurrentWizard.transform.position, CurrentWizard.Range, CurrentWizard.EnemiesMask);
        
        NativeArray<EnemyData> EnemiesToCalculate = new NativeArray<EnemyData>();

        
        return null;
    }

    struct EnemyData
    {
        public EnemyData(Vector3 position, int nodeindex, float hp){
            EnemyPosition = position;
            NodeIndex = nodeindex;
            Health = hp;
        }

        public Vector3 EnemyPosition;
        public int NodeIndex;
        public float Health;
        
    }
}
