using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject attackGMB;
    public Transform objPulling;

    List<GameObject> attackList     = new List<GameObject>();
    List<GameObject> attackListUsed = new List<GameObject>();

    public static AttackManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SpawnNewAttack()
    {
        GameObject  atk = Instantiate(attackGMB, new Vector3(999,999,0), Quaternion.identity);
                    atk.transform.parent = objPulling;
                    attackList.Add(atk);
    }

    public void BackToPullingList(GameObject obj)
    {
        obj.transform.position = new Vector3(999, 999, 0);
        attackListUsed.Remove(obj);
        attackList.Add(obj);
    }

    public void SpawnAttackAtTarget(FlockAgent targetAttacked, int damage)
    {
        if (attackList.Count == 0) return;

        GameObject  atk = attackList[0];
                    atk.transform.position = targetAttacked.transform.position;
                    UseAttack(atk);
                    SetAttack(atk, targetAttacked, damage);
    }

    void UseAttack(GameObject atk)
    {
        attackList.Remove(atk);
        attackListUsed.Add(atk);
        atk.GetComponent<RemoveEvent>().RemoveAfterDelay(1f);
    }

    void SetAttack(GameObject atk, FlockAgent targetAttacked, int damage)
    {
        AttackTarget atkTarget = atk.GetComponent<AttackTarget>();
        atkTarget.target = targetAttacked;
        atkTarget.damage = damage;
    }
}