using UnityEngine;

namespace GamePlay
{
    public interface IMotorControl
    {

        /// <summary>
        /// плавный поворот от акселерометра
        /// </summary>
        /// <param name="value">должен быть от 0 до 1</param>
        void TurnSmoothly(float value);

        void Jump(JumpDrection jumpDrection);

        /// <summary>
        /// </summary>
        /// <param name="value"> должен быть от 0 до 1</param>
        void Accelerate(float value);

    }

    public enum JumpDrection
    {
        Up = 0,
        Down=1
    }
}
