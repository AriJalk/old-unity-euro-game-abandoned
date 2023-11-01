using EDBG.GameLogic.Rules;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Utilities
{

    public static class UtilityFunctions
    {

        static UtilityFunctions()
        {

        }

        public static void ShuffleArray<T>(T[] array)
        {
            int length = array.Length;
            //Fischer-Yates implementation
            for (int i = length - 1; i > 0; i--)
            {
                int j = i + UnityEngine.Random.Range(0, length - i);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        public static void ShuffleList<T>(List<T> list)
        {
            int length = list.Count;
            //Fischer-Yates implementation
            for (int i = length - 1; i > 0; i--)
            {
                int j = i + UnityEngine.Random.Range(0, length - i);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public static PieceColors GetRandomComponentColor()
        {
            PieceColors color = PieceColors.White;
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    color = PieceColors.White;
                    break;
                case 1:
                    color = PieceColors.Blue;
                    break;
                case 2:
                    color = PieceColors.Green;
                    break;
                case 3:
                    color = PieceColors.Red;
                    break;
                default:
                    break;
            }
            return color;
        }

        public static void PrintStack<T>(Stack<T> stack)
        {
            Debug.Log("Print Stack");
            string stackText = string.Empty;
            foreach (T i in stack)
            {
                stackText += i.ToString();
            }
            Debug.Log(stackText + "\n");
        }

        public static void PrintArray<T>(T[] array)
        {
            Debug.Log("Print Array");
            string arrayText = string.Empty;
            foreach (T i in array)
            {
                arrayText += i.ToString();
            }
            Debug.Log(arrayText + "\n");
        }

    }

}
