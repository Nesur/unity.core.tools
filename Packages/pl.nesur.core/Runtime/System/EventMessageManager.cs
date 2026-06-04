using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nesur.Core.System {
    public class EventMessageManager : Singleton<EventMessageManager>, ITickable {
        /// <summary>
        /// Event that is triggered for example when a production event occurs. For example, when a unit is produced or when there is an error in production like no resources.
        /// </summary>
        public EventHandler<string> OnEventMessage;

        [SerializeField] float debounceTime = 1f;
        private float _debounceTimer = 0f;


        /// <summary>
        /// Message queue to store messages before they are processed and debaunce to deduplicate.
        /// </summary>
        private readonly List<string> _messageQueue = new();

        public void PostMessage(string message) {
            Logger.Log($"Posting message: {message}");
            TickManager.Instance.RegisterTickable(this);
            _messageQueue.Add(message);
            if (_debounceTimer <= 0) {
                _debounceTimer = debounceTime;
            }
        }

        public void OnTick() {
            if (_debounceTimer > 0) {
                _debounceTimer -= Time.deltaTime;
            }

            if (_debounceTimer <= 0) {
                ProcessQueue();
            }
        }

        private void ProcessQueue() {
            List<string> distinctMessages = _messageQueue.Distinct().ToList();
            distinctMessages.ForEach(message => { OnEventMessage?.Invoke(this, message); });
            _messageQueue.Clear();
            TickManager.Instance.UnregisterTickable(this);
        }
    }
}