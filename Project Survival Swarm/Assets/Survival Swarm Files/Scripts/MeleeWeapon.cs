using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public enum MeleeType { axe, pickaxe, combat }
    [SerializeField] private MeleeType meleeType;

    public ActiveWeapon.WeaponSlot meleeSlot;

    public float damageAmount;
    public bool canSwing;
    public string meleeName;

    [HideInInspector] public Animator rigController;

    public Transform damageCenter;
    public LayerMask damagableLayer;
    public float damageRadius;

    private Collider[] hitColliders = new Collider[10];

    void Start()
    {
        //Rig controller'ý equip de atadým
        canSwing = true;

    }

    public void SwingMelee()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canSwing)
            {
                rigController.Play("weapon_melee_attack", 1, 0f);
                canSwing = false;
                StartCoroutine(SwingDelay());
            }
        }
    }


    //todo Give damage yerine health.cs in içine TakeDamage fonsksiyonu oluþtur. Böylece her sýnýf için ayrý bir give damage olusturmayýz
    //todo Kaynak toplamak icin bir metod olusturup bu metodu kaynak toplama aracýna göre sekillendirebiliriz
    public void GiveDamage(float damage) //Animation Event
    {

        int numberOfColliders = Physics.OverlapSphereNonAlloc(damageCenter.position, damageRadius, hitColliders, damagableLayer);
        if (numberOfColliders > 0)
        {
            for (int i = 0; i < numberOfColliders; i++)
            {
                if (hitColliders[i] != null)
                {
                    if (hitColliders[i].TryGetComponent(out Health health))
                    {
                        if (hitColliders[i].CompareTag("Tree") && meleeType == MeleeType.axe)
                        {
                            ResourceHandler.Instance.wood += 10;
                            ResourceHandler.Instance.updateResourcesAction.Invoke();
                            
                            Debug.Log("Give Damage Çalýþtý");
                            health.currentHealth -= damage;
                        }
                        else if (hitColliders[i].CompareTag("ScrapMetal") && meleeType == MeleeType.pickaxe)
                        {
                            ResourceHandler.Instance.scrap_metal += 10;
                            ResourceHandler.Instance.updateResourcesAction.Invoke();

                            Debug.Log("Give Damage Çalýþtý");
                            health.currentHealth -= damage;
                        }
                        else if (hitColliders[i].CompareTag("Stone") && meleeType == MeleeType.pickaxe)
                        {
                            ResourceHandler.Instance.stone += 10;
                            ResourceHandler.Instance.updateResourcesAction.Invoke();

                            Debug.Log("Give Damage Çalýþtý");
                            health.currentHealth -= damage;
                        }
                    }
                }
            }

        }
    }

    IEnumerator SwingDelay()
    {
        yield return new WaitForSeconds(0.5f);//Sallama animasyonunun uzunlugu kadar beklet
        canSwing = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(damageCenter.position, damageRadius);
    }

}
