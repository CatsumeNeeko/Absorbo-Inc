using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Aoe Debuff",menuName ="Ability/Aoe Debuff")]
public class ShootAbility : AbilitySO
{
    public float shootCooldown , aoeCooldown;
    public GameObject projectile;
    public float projectileDuration;
    public float checkRange;
    public LayerMask enemyLayer;
    public float debuffDuration;
    [SerializeField] GameObject spawnedProjectile;
    private float[] enemySpeeds;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        if(spawnedProjectile == null)
        {
            FireProjectile(owner);
            cooldown = shootCooldown;
        }
        else
        {
            AoeCheck(spawnedProjectile);
            cooldown = aoeCooldown;
        }            
    }
    private void FireProjectile(GameObject owner)
    {
        Vector3 spawnPos = new Vector3(owner.transform.position.x, owner.transform.position.y + 2f, owner.transform.position.z);
        spawnedProjectile = Instantiate(projectile,spawnPos, Quaternion.identity);
        Physics.IgnoreCollision(spawnedProjectile.GetComponent<Collider>(), owner.GetComponent<Collider>());
        //Vector3 cameraDirection = Camera.main.transform.forward;

        //spawnedProjectile.GetComponent<Rigidbody>().velocity = cameraDirection * checkRange / projectileDuration;

        Destroy(spawnedProjectile, projectileDuration);
    }
    private void AoeCheck(GameObject spawnedProjectile)
    {

        Debug.Log("Aoe Check");
        Collider[] colliders = Physics.OverlapSphere(spawnedProjectile.transform.position, checkRange, enemyLayer);
        enemySpeeds = new float[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            
            EnemyStats enemy = colliders[i].GetComponent<EnemyStats>();
            Debug.Log(enemy);
            if (enemy != null)
            {
                enemySpeeds[i] = enemy.currentMovementSpeed; 
                enemy.currentMovementSpeed = 0;
                enemy.GetComponent<MonoBehaviour>().StartCoroutine(ResetSpeed(enemy, i));
            }
        }
        Destroy(spawnedProjectile);
    }
    private IEnumerator ResetSpeed(EnemyStats enemy,int index)
    {
        yield return new WaitForSeconds(debuffDuration);
        enemy.currentMovementSpeed = enemySpeeds[index];
    }
}
