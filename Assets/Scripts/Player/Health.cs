using System.Collections;
using FPSLabyrinth.UI;
using UnityEngine;

namespace FPSLabyrinth.Player
{
    public class Health : MonoBehaviour
    {
        // Material used for displaying blood effects
        [SerializeField] private Material fsBlood;
        // Reference to the UI menu for game over conditions
        [SerializeField] private MenuUI menuUI;
        // Maximum health value
        [SerializeField] private float maxHealth = 100;
        // Cooldown time before health regeneration starts
        [SerializeField] private float regenAfterCooldownInSeconds = 5f;
        // Amount of health restored per second during regeneration
        [SerializeField] private float healthRegenInSecond = 5f;
    
        // Indicates whether health regeneration is currently active
        private bool isRegenerating = false;
        // Current health value
        private float health = 100;

        // Initializes the health display at the start
        private void Start()
        {
            fsBlood.SetFloat("_Health", health / maxHealth);
        }

        // Coroutine to wait before starting health regeneration
        private IEnumerator WaitRegen()
        {
            yield return new WaitForSeconds(regenAfterCooldownInSeconds);
            isRegenerating = true;
        }

        // Updates health regeneration and blood effect each frame
        private void Update()
        {
            if (isRegenerating && health <= maxHealth)
            {
                health += healthRegenInSecond * Time.deltaTime;
                if(health > maxHealth) { health = maxHealth; }
                fsBlood.SetFloat("_Health", health / maxHealth);
            }
        }
    
        // Reduces health by a specified damage amount
        public void TakeDamage(float damage)
        {
            health -= damage;
            isRegenerating = false;
            SoundManager.Instance.PlaySoundClip(SoundManager.Instance.SoundConfig.playerHitSound, transform, 0.5f);
            StopAllCoroutines();
            StartCoroutine(WaitRegen());
            fsBlood.SetFloat("_Health", health / maxHealth);
            if (health <= 0) { menuUI.LoseUI.Show(); }
        }
    }
}
