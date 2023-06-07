using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehavior : MonoBehaviour
{
    /// <summary>
    /// CLASSE PARENT Des Personnages
    /// </summary>
    protected bool IsAgressif;
    protected bool IsPacif;
     protected Rigidbody2D rb;
    protected CircleCollider2D DetectZone;
   [SerializeField] protected float radius;
    protected float Distance;
    [SerializeField] protected int Speed;
    [SerializeField] protected private Collider2D anotherEntity;
    public List<CharacterData> CharacterData = new List<CharacterData>();
    private Object[] resources;
    public int Health;
    public int DMG;
    protected CharacterData data;
    protected PacifistesBehavior Pacifist;
    protected AgressifBehavior Agressif;
    /// <summary>
    /// Load des infos du scriptable correspondant
    /// </summary>
    protected abstract void LoadScriptable();
   

    protected void CheckHP()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Dmg)
    {
        this.Health -= Dmg;
        Debug.Log("TEST DMG");
    }

    protected virtual void CheckType(Collider2D other)
    {
       
        if (other.GetComponent<AgressifBehavior>() == true)
        {
            Agressif = other.GetComponent<AgressifBehavior>();
          
        }
        else if (other.GetComponent<PacifistesBehavior>())
        {
            Pacifist = other.GetComponent<PacifistesBehavior>();
            
        }
    }
}
