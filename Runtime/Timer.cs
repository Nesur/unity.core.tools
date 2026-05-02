using System;
using UnityEngine;

namespace Nesur.Core {
    public class Timer {
        public EventHandler<float> OnTimerUpdated;
        public EventHandler OnTimerFinished;
        public EventHandler OnTimerElapsed;
        private readonly float _duration;
        private readonly float _iterationCount;
        private float _currentTime = 0;
        private float _currentIteration = 0;
        private bool _paused = false;
        private bool _timerFinishedAllIterations;

        public Timer(float duration) {
            Debug.Log("Timer created with duration: " + duration);
            _duration = duration;
            _currentTime = 0;
            _iterationCount = 0;
        }

        public Timer(float duration, int iterationCount) {
            Debug.Log("Timer created with duration: " + duration + " iterationCount: " + iterationCount);
            _duration = duration;
            _currentTime = 0;
            _iterationCount = iterationCount;
        }


        public void Update() {
            if (_paused) {
                return;
            }

            if (_timerFinishedAllIterations) {
                return;
            }

            _currentTime += Time.deltaTime;
            if (_currentTime >= _duration) {
                OnTimerElapsed?.Invoke(this, EventArgs.Empty);

                if (_iterationCount != 0) {
                    _currentTime = 0;
                    _currentIteration++;
                    if (_currentIteration >= _iterationCount) {
                        _timerFinishedAllIterations = true;
                        OnTimerFinished?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            OnTimerUpdated?.Invoke(this, GetElapsedTimePercentage());
        }

        public void IncreaseTime(float timeInSeconds) {
            if (_currentTime >= _duration) {
                return;
            }
            _currentTime += timeInSeconds;
            if (_currentTime > _duration) {
                _currentTime = _duration;
            }
        }
        public void DecreaseTime(float timeInSeconds) {
            if (_currentTime <= 0) {
                return;
            }
            _currentTime -= timeInSeconds;
            if (_currentTime < 0) {
                _currentTime = 0;
            }
        }

        public void Reset() {
            _currentTime = 0;
            _currentIteration = 0;
            _timerFinishedAllIterations = false;
        }

        public float GetElapsedTimePercentage() {
            return _currentTime / _duration;
        }

        public float GetElapsedTimeInSeconds() {
            return _currentTime;
        }

        public void Unpause() {
            _paused = false;
        }

        public void Pause() {
            _paused = true;
        }
    }
}