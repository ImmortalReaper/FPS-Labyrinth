using UnityEngine;

namespace FPSLabyrinth.Configs
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Sound Config")]
    public class SoundConfig : ScriptableObject
    {
        public AudioClip pickUpKeySound;
        public AudioClip unlockLockSound;
        public AudioClip doorOpenSound;
        public AudioClip footstepSound;
        public AudioClip monsterRoarSound;
        public AudioClip playerHitSound;
    }
}
