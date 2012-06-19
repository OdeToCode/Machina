using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Machina.StateMachine
{
    internal class MethodSelector : IProxyGenerationHook
    {
        public void MethodsInspected()
        {            
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {         
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var methodName = methodInfo.Name;
            return !methodName.StartsWith("get_") &&
                   !methodName.StartsWith("set_") &&
                   methodInfo.IsPublic;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}