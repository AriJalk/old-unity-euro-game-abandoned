using EDBG.Engine.Core;
using EDBG.GameLogic.Rules;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Utilities
{

    public static class UtilityFunctions
    {

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

        public static List<T> GetShuffledList<T>(int amountOfEach, params T[] elements)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < elements.Length; i++)
            {
                for (int j = 0; j < amountOfEach; j++)
                {
                    list.Add(elements[i]);
                }
            }
            ShuffleList(list);
            return list;
        }


        public static PlayerColors GetRandomComponentColor()
        {
            PlayerColors color = PlayerColors.White;
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    color = PlayerColors.White;
                    break;
                case 1:
                    color = PlayerColors.Blue;
                    break;
                case 2:
                    color = PlayerColors.Green;
                    break;
                case 3:
                    color = PlayerColors.Red;
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
