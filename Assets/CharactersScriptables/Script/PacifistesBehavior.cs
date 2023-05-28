using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PacifistesBehavior : CharacterBehavior
{
    [SerializeField] float ConvertPercent = 0;
    [SerializeField] int MaxConvert;
    [SerializeField] float Multiplicater;

 
   
    bool HasConvert = false;
   
    // Start is called before the first frame update
    void Start()
    {
        LoadScriptable();
        HasConvert = false;
      
        MaxConvert = data.MaxConvert;
        Multiplicater = data.ConvertPercentMultiplicater;
        Speed = data.MaxSpeed;
        Health = data.Health;
        DMG = data.DMG;
        IsAgressif = false;
        IsPacif = true;
        DetectZone = this.GetComponentInChildren<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        DetectZone.radius = radius;
    }

    public void ChaseAnotherEntity(Collider2D other)
    {
        if (other.GetComponent<PacifistesBehavior>() != null)
        {
            Distance = Vector2.Distance(transform.position, other.transform.position);
            Vector2 direction = other.transform.position - transform.position;
            direction.Normalize();

            /// Pour que ça tourn de manière smoooooooooooooth
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.position = Vector2.MoveTowards(this.transform.position, other.transform.position, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
   
    private void Update()
    {
        CheckHP();
        
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
           anotherEntity = collision;
            HasConvert = false;
           
            if(anotherEntity != null)
            {
                  MaxConvert = 100;
                if (anotherEntity.GetComponentInParent<PacifistesBehavior>())
                {
               
                        Pacifist = anotherEntity.GetComponentInParent<PacifistesBehavior>();
                   
                    if (HasConvert == false)
                    {
                        StartCoroutine(ConvertOther(Pacifist));

                    }
                }
                
                
            }
    }


    void CheckHasConvert()
    {
        if (HasConvert == true)
        {
            if (Pacifist.ConvertPercent > 0)
            {
                Pacifist.ConvertPercent = 0;

                StopCoroutine(ConvertOther(Pacifist));
                MaxConvert = 0;
            }
        }
        
    }
   



    void OnTriggerExit2D(Collider2D collision)
    {


        HasConvert = true;
        if (collision != null && Pacifist.ConvertPercent >= 0)
        {
            HasConvert = true;


            CheckHasConvert();

        }

    }


    protected override void LoadScriptable()
    {
        data = Resources.Load<CharacterData>("CharactersData/Pacifistes/Pacifos");
       base.CharacterData.Add(data);
       
    }

    public IEnumerator ConvertOther( PacifistesBehavior Pacifist)
    {
        
            for(int i = 0; Pacifist.ConvertPercent < MaxConvert;i++)
            {
                 if(Pacifist != null)
                 {
                        Pacifist.ConvertPercent += Multiplicater;
                        yield return new WaitForSeconds(0.5f);
                           
                        
                 }
                    if(anotherEntity == null && Pacifist.ConvertPercent >= 0)
                    {
                        HasConvert = true;
                         break;
                    }
                
             
                    if(ConvertPercent == MaxConvert)
                    {
                        HasConvert = true;
                        
                            /// Call Fonction d'absorbption
                     

                        break;
                    }
            }


      
       
        
    }

}
