using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kitchen;
using KitchenData;
using Unity.Collections;
using Unity.Entities;

namespace BetterDisplayStands
{
    [HarmonyPatch(typeof(AssignMenuRequests))]
    public static class AssignMenuRequestsPatches
    {

        [HarmonyPatch("OnUpdate")]
        [HarmonyPrefix]
        public static void OnUpdate_PrefixPatch(EntityQuery ___Encouragers)
        {
            NativeArray<Entity> encouragers = ___Encouragers.ToEntityArray(Allocator.TempJob);

            foreach (Entity encourager in encouragers)
            {
                EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
                bool hasComponent = manager.HasComponent<CPreservesContentsOvernight>(encourager);

                if (ConfigData.EnableMod.Value)
                {
                    COrderEncourager oe = new COrderEncourager() { Probability = ConfigData.DisplayStandEffectiveness.Value };
                    manager.SetComponentData(encourager, oe);

                    if (ConfigData.EnableFrozenDisplayStands.Value)
                    {
                        if (!hasComponent)
                            manager.AddComponentData(encourager, new CPreservesContentsOvernight());
                    }
                    else
                    {
                        if (hasComponent)
                        {
                            manager.RemoveComponent<CPreservesContentsOvernight>(encourager);
                        }
                    }
                }
                else
                {
                    COrderEncourager oe = new COrderEncourager() { Probability = 0.8f };
                    World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(encourager, oe);

                    if (hasComponent)
                    {
                        manager.RemoveComponent<CPreservesContentsOvernight>(encourager);
                    }
                }
            }
        }
    }
}
