using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class WizardActions : MonoBehaviour
{
    public LayerMask EnemiesLayer;
    public Enemy Target;
    public Transform WizardPivot;
    public float Damage;
    public float Firerate;
    public float Range;
    public float Delay;

    void Start()
    {
        Delay = 1/Firerate;
    }


    public void Tick()
    {
        if(Target != null)
        {
            WizardPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        }
    }
}
