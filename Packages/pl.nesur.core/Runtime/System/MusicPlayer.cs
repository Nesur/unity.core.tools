using UnityEngine;

namespace Nesur.Core.System {
    public class MusicPlayer : Singleton<MusicPlayer> {
        private AudioSource _musicSource;

        public void StopMusic() {
            _musicSource.Stop();
        }
        
        public void PlayMusic() {
            _musicSource.Play();
        }
        
        public void SetMusicVolume(float value) {
            _musicSource.volume = value;
        }

        public void SetMute(bool isOn) {
            _musicSource.mute = isOn;
        }

        protected override void Awake() {
            base.Awake();
            _musicSource = GetComponent<AudioSource>();
        }
    }
}