using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public class AgressifBehavior : CharacterBehavior
{
  
    private Transform Target;
    [SerializeField] float LauchForce;
    [SerializeField] float ChargedTime;
    [SerializeField] float RepulseForce;
    private bool IsLaunch = false;
    private Vector2 DistancePos;

 
    private void Awake()
    {
        LoadScriptable();
    }

    private void Start()
    {
       
        Health = data.Health;
         DMG = data.DMG;
        IsAgressif = true;
        IsPacif = false;
        DetectZone = this.GetComponentInChildren<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        DetectZone.radius = radius;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            anotherEntity = collision;
            Target = anotherEntity.transform;      
            StartCoroutine(ChargedAttack());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(anotherEntity != null)
        {
            anotherEntity = null;
        }
    }
   
    private void Update()
    {
        CheckLauch();
        CheckHP();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        CheckAttack();
    }

    /// <summary>
    /// Fonction qui permet au perso de charger dans la direction de sa cible
    /// </summary>
    public IEnumerator Charge()
    {
            CheckType(anotherEntity);
        if (anotherEntity.GetComponent<AgressifBehavior>() != null || anotherEntity.GetComponent<PacifistesBehavior>() != null) 
        {
             float LaucnhDirectionX = Target.position.x;
          float LaucnhDIrectionY = Target.position.y;
            float posX = transform.position.x;
            float posY = transform.position.y;

            Vector2 lauchDir = new Vector2(LaucnhDirectionX - posX, LaucnhDIrectionY - posY);
            DistancePos = lauchDir;
            rb.velocity = lauchDir * LauchForce;
            IsLaunch = true;

            yield return new WaitForSeconds(ChargedTime);
            IsLaunch = false;


        }
    }

  
    public void CheckAttack()
    {
        if (Agressif != null)
        {
            Agressif.TakeDamage(Agressif.DMG);

        }
        else if (Pacifist != null)
        {
            Pacifist.TakeDamage(Pacifist.DMG);
        }
        if (DistancePos != null)
        {
            rb.velocity = -DistancePos * RepulseForce;
        }
    }

    

    public void CheckLauch()
    {
        if (IsLaunch)
        {
            DetectZone.enabled = false;
        }
        if (IsLaunch == false)
        {
            DetectZone.enabled = true;
            rb.velocity = new Vector2 (0, 0);
        }
    }
  

  
    /// <summary>
    /// Delay permettant de "charger"
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChargedAttack()
    {
        
        yield return new WaitForSeconds(ChargedTime);
        if (anotherEntity != null)
        {

            StartCoroutine(Charge());
        }
    }
   
   protected override void LoadScriptable()
   {
        data = Resources.Load<CharacterData>("CharactersData/Agressifs/Agressos");
        base.CharacterData.Add(data);
        
   }
}
