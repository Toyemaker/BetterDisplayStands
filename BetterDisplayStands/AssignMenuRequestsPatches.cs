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
                COrderEncourager oe = new COrderEncourager() { Probability = ConfigData.DisplayStandEffectiveness.Value };
                World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(encourager, oe);
            }
        }
    }
}
