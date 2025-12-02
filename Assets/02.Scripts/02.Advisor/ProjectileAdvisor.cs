using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAdvisor : MonoBehaviour
{
    public AdvisorStat Stat { get; private set; }
    public ProjectileAdvisorController Controller { get; private set; }

    private void Awake()
    {
        Stat = GetComponent<AdvisorStat>();
        Controller = GetComponent<ProjectileAdvisorController>();
    }
}
