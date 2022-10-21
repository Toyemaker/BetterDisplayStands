using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetterDisplayStands
{
    public static class Helpers
    {
        public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
        where T : Delegate
        {
            IntPtr ptr = method.MethodHandle.GetFunctionPointer();
            return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
        }
    }
}
