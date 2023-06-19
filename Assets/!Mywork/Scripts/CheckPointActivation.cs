using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointActivation : MonoBehaviour
{
    public GameObject NextCheckPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (LevelsDataManager.Instance.currentGameMode == gameModesEnum.Career)
            {
                CarrierModeManager.Instance.ActivateCheckpointParticles();
            }
            if (LevelsDataManager.Instance.currentGameMode == gameModesEnum.Obstacle)
            {
                ObstacleModeManager.Instance.ActivateCheckpointParticles();
            }


            if (NextCheckPoint != null)
            {
                NextCheckPoint.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
