/// <summary>
/// Created by Leo CUSSERNE
/// A class with utility functions for turrets.
/// </summary>

namespace LeoCusserneAPI
{
    using UnityEngine;


    public class TurretUtilityFunctions
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }
        /// <summary>
        /// Predict the direction the shot should take to hit the target depending on the projectile speed
        /// </summary>
        /// <param name="targetOrigin"> Where the projectile should hit</param>
        /// <param name="targetVelocity">The target's velocity</param>
        /// <param name="shotOrigin">Where the projectile is fired</param>
        /// <param name="projectileSpeed">The projectile's speed</param>
        /// <returns></returns>
        public Vector3 PredictShotDirection(Vector3 targetOrigin, Vector3 targetVelocity, Vector3 shotOrigin, float projectileSpeed)
        {
            Vector3 directionToTarget = (targetOrigin - shotOrigin).normalized;
            Vector3 targetVelocityOrtho = directionToTarget * Vector3.Dot(targetVelocity, directionToTarget);
            Vector3 targetVelocityTang = targetVelocity - targetVelocityOrtho;
            Vector3 shotVelTang = targetVelocityTang;
            float shotVelSpeed = shotVelTang.magnitude;
            if (shotVelSpeed > projectileSpeed)
            {
                return targetVelocity.normalized * projectileSpeed;
            }
            else
            {
                float shotSpeedOrtho = Mathf.Sqrt(Mathf.Sqrt(projectileSpeed) - Mathf.Sqrt(shotVelSpeed));
                Vector3 ShotVelOrtho = directionToTarget * shotSpeedOrtho;
                return ShotVelOrtho + shotVelTang;
            }
        }

        /// <summary>
        /// Rotate on given axis a turret's element to align his forward vector with a given direction.
        /// </summary>
        /// <param name="newRotation">Return the new rotation of the turret's element</param>
        /// <param name="isTurretCorrectlyAligned">Return is the turret's element is correctly aligned with the target direction</param>
        /// <param name="targetDirection">The direction with which the turret's element should align</param>
        /// <param name="turretElementGameObject">The gameObject of the turret's element (can ba a cannon or the support of the cannon for example</param>
        /// <param name="rotationAxis">The local axis on which the turret's element will rotate</param>
        /// <param name="isInterpolating">Is the rotation interpolated (smoothed)</param>
        /// <param name="startTime">The Time.time when the turret's element rotation is started (leave it to 0 if not using interpolation)</param>
        /// <param name="interpolationDuration">The time the turret's element will take to make his rotation</param>
        /// <param name="alignmentTolerence">The angle tolerence with which we consider that the turret's element is correctly aligned</param>
        public void RotateTurretComponent
            (
            out Quaternion newRotation,
            out bool isTurretCorrectlyAligned,
            Vector3 targetDirection,
            GameObject turretElementGameObject,
            Axis rotationAxis,
            bool isInterpolating,
            float startTime,
            float interpolationDuration,
            float alignmentTolerence
            )
        {
            Transform targetTransform = null;
            targetTransform.position = turretElementGameObject.transform.position + targetDirection.normalized * 300;
            Transform localTransform = null;
            localTransform.LookAt(targetTransform);
            Quaternion localRotation = localTransform.rotation;
            float t = (Time.time - startTime) / interpolationDuration;
            switch (rotationAxis)
            {
                case Axis.X:
                    {
                        float x = Mathf.SmoothStep(turretElementGameObject.transform.localRotation.x, localRotation.x, t);
                        float y = turretElementGameObject.transform.localRotation.y;
                        float z = turretElementGameObject.transform.localRotation.z;
                        turretElementGameObject.transform.localRotation = Quaternion.Euler(x, y, z);
                        newRotation = Quaternion.Euler(x, y, z);
                        if (x <= localRotation.x + alignmentTolerence && x >= localRotation.x - alignmentTolerence && y <= localRotation.y + alignmentTolerence && y >= localRotation.y - alignmentTolerence && z <= localRotation.z + alignmentTolerence && z >= localRotation.z - alignmentTolerence)
                        {
                            isTurretCorrectlyAligned = true;
                            break;
                        }
                        isTurretCorrectlyAligned = false;
                        break;
                    }
                case Axis.Y:
                    {
                        float x = turretElementGameObject.transform.localRotation.x;
                        float y = Mathf.SmoothStep(turretElementGameObject.transform.localRotation.y, localRotation.y, t);
                        float z = turretElementGameObject.transform.localRotation.z;
                        turretElementGameObject.transform.localRotation = Quaternion.Euler(x, y, z);
                        newRotation = Quaternion.Euler(x, y, z);
                        if (x <= localRotation.x + alignmentTolerence && x >= localRotation.x - alignmentTolerence && y <= localRotation.y + alignmentTolerence && y >= localRotation.y - alignmentTolerence && z <= localRotation.z + alignmentTolerence && z >= localRotation.z - alignmentTolerence)
                        {
                            isTurretCorrectlyAligned = true;
                            break;
                        }
                        isTurretCorrectlyAligned = false;
                        break;
                    }
                case Axis.Z:
                    {
                        float x = turretElementGameObject.transform.localRotation.x;
                        float y = turretElementGameObject.transform.localRotation.y;
                        float z = Mathf.SmoothStep(turretElementGameObject.transform.localRotation.z, localRotation.z, t);
                        turretElementGameObject.transform.localRotation = Quaternion.Euler(x, y, z);
                        newRotation = Quaternion.Euler(x, y, z);
                        if (x <= localRotation.x + alignmentTolerence && x >= localRotation.x - alignmentTolerence && y <= localRotation.y + alignmentTolerence && y >= localRotation.y - alignmentTolerence && z <= localRotation.z + alignmentTolerence && z >= localRotation.z - alignmentTolerence)
                        {
                            isTurretCorrectlyAligned = true;
                            break;
                        }
                        isTurretCorrectlyAligned = false;
                        break;
                    }
                default:
                    {
                        newRotation = Quaternion.Euler(0, 0, 0);
                        isTurretCorrectlyAligned = false;
                        break;
                    }
            }
        }
    }
}

