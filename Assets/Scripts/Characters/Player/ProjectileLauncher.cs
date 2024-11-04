using System;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    public void LaunchProjectile() {
        //TODO : Add a pool for the projectile objects
        GameObject projectile = Instantiate(this.projectilePrefab, this.launchPoint.position, this.launchPoint.transform.rotation);
        Vector3 scale = projectile.transform.localScale;
        Single directionX = scale.x * transform.localScale.x > 0 ? 1 : -1;
        projectile.transform.localScale = new Vector3(directionX, scale.y, scale.z);
    }
}
