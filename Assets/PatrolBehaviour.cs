using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints; // Массив точек патрулирования
    [SerializeField] private float _moveSpeed = 2f; // Скорость перемещения в м/с
    [SerializeField] private float _waitTime = 2f; // Время ожидания в секундах

    private int _currentPatrolIndex = 0;
    private float _elapsedTime = 0f;
    private bool _isWaiting = false;

    private void Update()
    {
            var start = transform.position;
            var end = _patrolPoints[_currentPatrolIndex].position;

            var distance = Vector3.Distance(start, end);
            var travelTime = distance / _moveSpeed;

            // Если ожидание
            if (_isWaiting)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= _waitTime)
                {
                    _isWaiting = false;
                    _elapsedTime = 0f;

                    // Переход к следующей точке
                    _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(start, end, Time.deltaTime / travelTime);
                
                if (Vector3.Distance(transform.position, end) < 0.01f)
                {
                    _isWaiting = true;
                }
            }
    }
}