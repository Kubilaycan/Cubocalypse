using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "PowerUp") {
            PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
            
            switch (powerUp.target)
            {
                case PowerUpTarget.Weapon:
                    gameObject.GetComponent<PlayerWeaponController>().weapon.ApplyPowerUp(powerUp.type, powerUp.value);
                    break;
                case PowerUpTarget.Player:
                    
                    switch (powerUp.type)
                    {
                        case PowerUpType.SprintBoost:
                            gameObject.GetComponent<PlayerMovementController>().IncreaseMovementSpeedByPercentage(powerUp.value);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
