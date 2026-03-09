

using System;
using UnityEngine;

namespace Karianakis.EditTools
{
    public static class TypesUtilities
    {

        public static bool TryConvertStringToType(string input, Type targetType, out object result , bool supressErrors )
        {
            try
            {
                result = Convert.ChangeType(input, targetType);
                return true;
            }
            catch (Exception ex)
            {
                if (!supressErrors)
                {
                    Debug.LogError($"Failed to convert '{input}' to type {targetType}: {ex.Message}");
                }

                result = null;
                return false;
            }
        }



    }

}