using UnityEngine;
using System.Collections;
public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    private bool CanTakeDamage = true;
    private Animator animator;
    private LogicScript logicScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MaxHealth;
        animator = GetComponentInParent<Animator>();
        logicScript = Object.FindFirstObjectByType<LogicScript>();
        logicScript.UpudatePlayerHP(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float Damage)
    {
        if (!CanTakeDamage)
            return;     
        Health -= Damage;
        logicScript.UpudatePlayerHP(Health);
        if (Health <= 0)
        {
            animator.SetBool("Death", true);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput>().OnDisable();
            Debug.Log("Player is Dead");
        }
        StartCoroutine(DamagePrevention());
    }
    private IEnumerator DamagePrevention() 
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if(Health > 0)
        {
            CanTakeDamage = true;
        }    
    }
}
