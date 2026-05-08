using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Nesur.Core.Util {
    public static class Utils {
        public static T GetRandomListElement<T>(IList<T> list) {
            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        public static bool TryGetRandomListElement<T>(IList<T> list, out T element) {
            if (list == null || list.Count == 0) {
                element = default;
                return false;
            }

            var randomIndex = Random.Range(0, list.Count);
            element = list[randomIndex];
            return true;
        }

        public static Vector3 GetMouseWorldPosition() {
            return GetMouseWorldPosition(Camera.main);
        }

        public static Vector3 GetMouseWorldPosition(Camera camera) {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            var raycast = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
            if (raycast) {
                return hit.point;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMouseWorldPosition(Camera camera, LayerMask layerMask) {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            var raycast = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask);
            if (raycast) {
                return hit.point;
            }

            return Vector3.zero;
        }

        public static void DrawRay(Vector3 destination, Color color) {
            var screenPointToRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(screenPointToRay.origin, destination, color);
        }

        public static Quaternion GetRotationTowardsTargetInDegrees(Vector3 originPosition, Vector3 targetPosition) {
            var direction = targetPosition - originPosition;
            var rad2Deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, rad2Deg, 0);
        }

        public static Quaternion GetRotationTowardsTarget(Transform rotatedTransform, Vector3 targetPosition) {
            var direction = targetPosition - rotatedTransform.position;
            float speed = 1f;
            float singleStep = speed * Time.deltaTime;
            Vector3 rotateTowards = Vector3.RotateTowards(rotatedTransform.forward, direction, singleStep, 0f);
            return Quaternion.LookRotation(rotateTowards);
        }

        public static Vector3 GenerateRandomDirectionNormalized() {
            var direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            return direction.normalized;
        }

        public static Vector3 GenerateRandomDirection2dNormalized() {
            var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            return direction.normalized;
        }

        public static Vector3 GenerateRandomHorizontalDirectionNormalized() {
            var direction = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
            return direction.normalized;
        }


        public static bool IsInRange(Vector3 currentPosition, Vector3 targetPosition, float distanceThreshold) {
            var distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
            return distanceToTarget <= distanceThreshold;
        }


        /// <summary>
        /// Generates random number and checks it against given threshold
        /// </summary>
        /// <param name="chancePercentageThreshold"></param>
        /// <returns>true if random number that was generated hit the given threshold, else false</returns>
        public static bool GenerateRngChance(float chancePercentageThreshold) {
            float generatedChancePercentage = Random.Range(0f, 1f);
            return generatedChancePercentage <= chancePercentageThreshold;
        }

        /// <summary>
        /// Check if collider belongs to given layer mask. If yes, then there have been collision with proper layer mask object
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collisionMask"></param>
        /// <returns>true if collision has been made and object is on the layer mask, else false</returns>
        public static bool CheckCollision(Collider collider, LayerMask collisionMask) {
            return CheckCollision(collider.gameObject.layer, collisionMask);
        }

        /// <summary>
        /// Check if 2D collider belongs to given layer mask. If yes, then there have been collision with proper layer mask object
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collisionMask"></param>
        /// <returns>true if collision has been made and object is on the layer mask, else false</returns>
        public static bool CheckCollision2D(Collider2D collider, LayerMask collisionMask) {
            return CheckCollision(collider.gameObject.layer, collisionMask);
        }

        private static bool CheckCollision(int objectLayer, LayerMask collisionMask) {
            return (collisionMask.value & (1 << objectLayer)) != 0;
        }

        public static Vector3 GenerateRandomPositionWithinBounds(Bounds spawnBounds) {
            var spawnAreaCenter = spawnBounds.center;
            var spawnAreaSize = spawnBounds.size;
            float x = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
            float y = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
            return new Vector3(x, y, 0);
        }

        /// <summary>
        /// Formats time in seconds to "mm:ss" format. For example, 90 seconds will be formatted as "01:30"
        /// </summary>
        /// <param name="timeInSeconds"></param>
        /// <returns></returns>
        public static string FormatMinutesAndSecondsFromTotalSeconds(float timeInSeconds) {
            int totalTimeInSeconds = Mathf.FloorToInt(timeInSeconds);
            int minutes = totalTimeInSeconds / 60;
            int seconds = totalTimeInSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }

        /// <summary>
        /// Formats time in seconds to "hh:mm:ss" format. For example, 90 seconds will be formatted as "00:01:30"
        /// </summary>
        /// <param name="timeInSeconds"></param>
        /// <returns></returns>
        public static string FormatHoursMinutesSecondsFromTotalSeconds(float timeInSeconds) {
            int totalTimeInSeconds = Mathf.FloorToInt(timeInSeconds);
            int hours = totalTimeInSeconds / 3600;
            int minutes = (totalTimeInSeconds % 3600) / 60;
            int seconds = totalTimeInSeconds % 60;
            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        public static Vector3 GenerateRandomPositionWithinBounds(ScreenBounds screenBounds) {
            return GenerateRandomPositionWithinBounds(screenBounds.GetBounds());
        }

        public static void RegisterPointerEnterEventTrigger(EventTrigger eventTrigger,
            UnityAction<BaseEventData> onPointerEnter) {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener(onPointerEnter);
            eventTrigger.triggers.Add(entry);
        }

        public static void RegisterPointerExitEventTrigger(EventTrigger eventTrigger,
            UnityAction<BaseEventData> onPointerExit) {
            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener(onPointerExit);
            eventTrigger.triggers.Add(exitEntry);
        }

        public static void ClearEventTriggers(EventTrigger eventTrigger) {
            eventTrigger.triggers.Clear();
        }
    }

    public readonly struct ScreenBounds {
        private readonly Vector2 _min;
        private readonly Vector2 _max;

        public ScreenBounds(Vector2 min, Vector2 max) {
            _min = min;
            _max = max;
        }

        public Bounds GetBounds() {
            return new Bounds((_min + _max) / 2, new Vector3(_max.x - _min.x, _max.y - _min.y, 0));
        }
    }

    [Serializable]
    public class SerializableDateTime {
        public string value;

        public DateTime ToDateTime() => DateTime.Parse(value);

        public static SerializableDateTime From(DateTime dt)
            => new SerializableDateTime { value = dt.ToString("o") };
    }
}