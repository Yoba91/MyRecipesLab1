using System;

namespace MyRecipesLab1.Core
{
    /// <summary>
    /// Генератор для уникального идентификатора
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Из двух уникальных Guid составляет один и возвращает его
        /// </summary>
        /// <returns></returns>
        public static long GenerateId()
        {
            var guidHashFirst = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();
            var guidHashSecond = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

            var firstPart = guidHashFirst.Substring(0, guidHashFirst.Length < 8 ? guidHashFirst.Length : 8);
            var secondPart = guidHashSecond.Substring(0, guidHashSecond.Length < 8 ? guidHashSecond.Length : 8);

            return long.Parse($"{firstPart}{secondPart}");
        }
    }
}
