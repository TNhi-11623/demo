using System;
using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class CarController : MonoBehaviour
    {
        public enum WheelType
        {
            FrontLeft,
            FrontRight,
            BackLeft,
            BackRight
        }

        [Serializable]
        public struct Wheel
        {
            public Transform wheelTransform;
            public WheelCollider wheelCollider;
            public WheelType wheelType;
            public float maxSteerAngle;
            public float maxMotorTorque;
        }

        [SerializeField] private List<Wheel> wheels;
        [SerializeField] private float maxSpeed = 100f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float brakeForce = 50f;
        private float currentSpeed = 0f;

        private void Update()
        {
            HandleInput();
            UpdateWheels();
        }

        private void HandleInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Tính currentSpeed một lần
            float speed = verticalInput * acceleration * Time.deltaTime;
            currentSpeed += speed;
            currentSpeed = Mathf.Clamp(currentSpeed, -10f, maxSpeed);

            for (int i = 0; i < wheels.Count; i++)
            {
                Wheel wheel = wheels[i];
                wheel.wheelCollider.steerAngle = horizontalInput * wheel.maxSteerAngle;
                wheel.wheelCollider.motorTorque = currentSpeed * wheel.maxMotorTorque;
                
                if (Input.GetKey(KeyCode.Space))
                {
                    wheel.wheelCollider.motorTorque = 0;
                    wheel.wheelCollider.brakeTorque = brakeForce;
                }
                else
                {
                    wheel.wheelCollider.brakeTorque = 0f;
                }
                wheels[i] = wheel; // Cập nhật lại danh sách
            }
        }

        private void UpdateWheels()
        {
            foreach (var wheel in wheels)
            {
                float rotationAngle = currentSpeed * Time.deltaTime;
                wheel.wheelTransform.Rotate(Vector3.right * rotationAngle);
                Vector3 position;
                Quaternion rotation;
                wheel.wheelCollider.GetWorldPose(out position, out rotation);
                wheel.wheelTransform.position = position;
                wheel.wheelTransform.rotation = rotation;
            }
        }
    }
}