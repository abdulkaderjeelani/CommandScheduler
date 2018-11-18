using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace CommandScheduler.Utilities.IoC.AutoFac
{
    public static class AutowiringNonPublicPropertyInjector
    {
        public static void InjectProperties(IComponentContext context,
               object instance, bool overrideSetValues)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (instance == null)
                throw new ArgumentNullException("instance");
            foreach (
               PropertyInfo propertyInfo in
                   //BindingFlags.NonPublic flag added for non public properties
                   instance.GetType().GetProperties(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic))
            {
                Type propertyType = propertyInfo.PropertyType;
                if ((!propertyType.IsValueType || propertyType.IsEnum) &&
                    (propertyInfo.GetIndexParameters().Length == 0 &&
                        context.IsRegistered(propertyType)))
                {
                    //Changed to GetAccessors(true) to return non public accessors
                    MethodInfo[] accessors = propertyInfo.GetAccessors(true);
                    if ((accessors.Length != 1 ||
                        !(accessors[0].ReturnType != typeof(void))) &&
                         (overrideSetValues || accessors.Length != 2 ||
                         propertyInfo.GetValue(instance, null) == null))
                    {
                        object obj = context.Resolve(propertyType);
                        propertyInfo.SetValue(instance, obj, null);
                    }
                }
            }
        }
    }
}
