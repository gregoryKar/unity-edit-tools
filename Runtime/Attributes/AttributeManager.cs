using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Karianakis.EditTools
{
    public static class AttributeManager
    {



        [RuntimeInitializeOnLoadMethod
            (RuntimeInitializeLoadType.AfterSceneLoad)]
        static void LoadAttributes()
        {
            var behavioursArray = GameObject.FindObjectsByType<MonoBehaviour>
  (FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            ScanVariables(behavioursArray);
            RegisterCheats(behavioursArray);
        }

        public static void ScanVariables(MonoBehaviour[] behavioursArray)
        {

            foreach (var behaviour in behavioursArray)
            {
                Type type = behaviour.GetType();

                FieldInfo[] fieldInfoArray = type.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic
                );


                foreach (var field in fieldInfoArray)
                {

                    DebugVariable debugAttribute = field.GetCustomAttribute<DebugVariable>();

                    if (debugAttribute == null) continue;


                    var myDebug = DynamicDebug.Create(field.Name);


                    if (debugAttribute.GetHasColor)
                        myDebug.SetColor(debugAttribute.GetColor);

                    // if (debugAttribute.GetTagThisGameobject)
                    //     myDebug.TagObject(behaviour.gameObject);
                    // else if (debugAttribute.GetHasTag)
                    //     myDebug.TagCode(debugAttribute.GetTag);



                    float interval;
                    if (debugAttribute.GetHasInterval) interval
                        = debugAttribute.GetInterval;
                    else interval = 0.25f;


                    WeakReference<MonoBehaviour> weakRef = new WeakReference
                                         <MonoBehaviour>(behaviour);


                    bool showMonoBehaviourNames = EditToolsSettings
                        .GetDynamicDebugShowMonobehaviourNames;
                    if (showMonoBehaviourNames)
                        myDebug.SetNickname($"{behaviour.name}.{field.Name}");
                    else
                        myDebug.SetNickname($"{field.Name}");


                    myDebug
                            .SetDynamicContent(() =>
                            {

                                if (weakRef.TryGetTarget(out var target)
                                    && target != null)
                                    return field.GetValue(target).ToString();
                                //$"{myDebug.GetNickname} : " + 

                                else
                                    return "field disposed or destroyed";
                            })
                            .SetDynamicEnabled(() =>
                            {
                                if (weakRef.TryGetTarget(out var target)
                                    && target != null)
                                    return true;
                                else
                                    return false;
                            })
                            .SetUpdate(interval);


                    //this was for no interval now default is i make it have by defaulat an interval and if the user set it in attribute it will use that if not it will use 0.25f
                    // }
                    //     else
                    //     myDebug.SetContnent(field.GetValue(behaviour).ToString()
                    //         );//myDebug.GetNickname + " : " +

                }
            }
        }

        static void RegisterCheats(MonoBehaviour[] behaviours)
        {

            foreach (var behaviour in behaviours)
            {
                var methods = behaviour.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var method in methods)
                {
                    var attr = method.GetCustomAttribute<ConsoleCommand>();
                    if (attr == null) continue;


                    string commandName;
                    if (attr.GetHasNickname)
                        commandName = attr.GetCommandName;
                    else
                        commandName = method.Name;


                    var parameters = method.GetParameters();
                    int paramCount = parameters.Length;
                    Delegate del;

                    if (paramCount == 0)
                    {
                        del = Delegate.CreateDelegate(typeof(Action), behaviour, method);
                    }
                    else
                    {
                        var paramTypes = parameters.Select(p => p.ParameterType).ToArray();
                        var delegateType = Type.GetType($"System.Action`{paramCount}").MakeGenericType(paramTypes);
                        del = Delegate.CreateDelegate(delegateType, behaviour, method);
                    }

                    var command = CommandBuilderUniversal.Create(commandName, del);
                  
                }
            }
        }



        static void testWeakReference(FieldInfo field, MonoBehaviour target)
        {
            // var weakRef = new WeakReference<MonoBehaviour>(target);

            // return () =>
            // {
            //     if (weakRef.TryGetTarget(out var instance) && instance != null)
            //     {
            //         return field.GetValue(instance);
            //     }

            //     return null; // object destroyed or collected
            // };


        }










    }







}
