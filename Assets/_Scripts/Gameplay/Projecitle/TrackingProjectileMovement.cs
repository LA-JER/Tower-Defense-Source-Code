using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static StatManager;
using static Targeting;
using static UnityEngine.UI.Image;

public class TrackingProjectileMovement : ProjectileMovement
{
    [SerializeField] CircleCollider2D rangeCollider;
    [SerializeField] private TargetingStyle targetingStyle;
    [SerializeField] private float coastSpeedRatio;

    private void Awake()
    {
        usesFixedUpdate = true;
    }

    private void Start()
    {
        if(projectile != null)
        {
            if(rangeCollider != null)
            {
                rangeCollider.radius = projectile.GetStat(Stat.projectileTargetRadius);
            }
        }
    }

    public override void MoveProjectile()
    {
        //FindTargetsWithSphereCast(transform.position, Vector3.forward, range, range, layerMask);
        if(projectile == null) { return; }

        //get 1 target based on chosen targeting style
        List<Transform> target = TargetingChip(targets, targetingStyle, 1, transform, rangeCollider.radius);
        if (target != null && target.Count >0)
        {
            Vector2 direction = (target[0].position - transform.position).normalized;
            //Vector2 direction = (new Vector2(target.position.x, target.position.y) - GetRigidbody2D().position).normalized;
            projectile.rb.velocity = (direction * projectile.GetStat(Stat.projectileSpeed));
            


            float angle = Mathf.Atan2(direction.y, direction.x);


            float angleDegrees = angle * Mathf.Rad2Deg + 270f;
            //float angleDeviatedDegrees = angleDeviated * Mathf.Rad2Deg + 270f;

            // Smoothly rotate the actor toward the targetsInRange
            projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, Quaternion.Euler(0, 0, angleDegrees), projectile.GetStat(Stat.trackingStrength) * Time.deltaTime);
        } else
        {
            //projectile.rb.velocity = Vector2.up * ( projectile.GetStat(Stat.projectileSpeed) * coastSpeedRatio);
            projectile.transform.Translate(Vector2.up * projectile.GetStat(Stat.projectileSpeed) * coastSpeedRatio * Time.deltaTime);
        }
    }

}
