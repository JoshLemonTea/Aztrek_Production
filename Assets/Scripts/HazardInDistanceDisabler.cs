using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class HazardInDistanceDisabler
{
    public FireTrap[] FireTraps;
    public LightningSpawner[] LightningSpawners;
    public Tornado[] Tornados;
    public WindCurrent[] WindCurrents;
    public Transform Player;

    private float _distanceToDisableHazards = 120;

    public void DisableHazardsInDistance()
    {
        foreach(FireTrap f in FireTraps)
        {
            if (Vector3.Distance(Player.position, f.transform.position) >= _distanceToDisableHazards)
                f.gameObject.SetActive(false);
            else
                f.gameObject.SetActive(true);
        }
        foreach (LightningSpawner l in LightningSpawners)
        {
            if (Vector3.Distance(Player.position, l.transform.position) >= _distanceToDisableHazards)
                l.gameObject.SetActive(false);
            else
                l.gameObject.SetActive(true);
        }
        foreach (Tornado t in Tornados)
        {
            if (Vector3.Distance(Player.position, t.transform.position) >= _distanceToDisableHazards)
                t.gameObject.SetActive(false);
            else
                t.gameObject.SetActive(true);
        }
        foreach (WindCurrent w in WindCurrents)
        {
            if (Vector3.Distance(Player.position, w.transform.position) >= _distanceToDisableHazards)
                w.gameObject.SetActive(false);
            else
                w.gameObject.SetActive(true);
        }
    }
}
