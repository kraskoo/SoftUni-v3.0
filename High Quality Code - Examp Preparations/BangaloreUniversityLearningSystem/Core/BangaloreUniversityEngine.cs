namespace BangaloreUniversityLearningSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BangaloreUniversityLearningSystem.Controllers;
    using BangaloreUniversityLearningSystem.Data;
    using BangaloreUniversityLearningSystem.Interfaces;
    using BangaloreUniversityLearningSystem.Models;

    public class Engine : IEngine
    {
        public void Run()
        {
            var database = new BangaloreUniversityData();
            User user = null;
            while (true)
            {
                string str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    break;
                }
                var route = new Route(str);
                var controller = Assembly.GetExecutingAssembly().GetTypes();
                Type cType = null;
                foreach (Type type in controller)
                {
                    if (type.Name == route.ControllerName)
                    {
                        cType = type;
                        break;
                    }
                }
                //var controllerType = Assembly.GetExecutingAssembly().GetTypes()
                //    .FirstOrDefault(type => type.Name == route.ControllerName);
                var ctrl = Activator.CreateInstance(cType, database, user) as Controller;
                var act = cType.GetMethod(route.ActionName);
                object[] @params = MapParameters(route, act);
                try
                {
                    var view = act.Invoke(ctrl, @params) as IView;
                    Console.WriteLine(view.Display());
                    user = ctrl.User;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private static object[] MapParameters(Route route, MethodInfo action)
        {
            ICollection<object> parameters = new List<object>();

            foreach (ParameterInfo parameter in action.GetParameters())
            {
                if (parameter.ParameterType == typeof(int))
                {
                    parameters.Add(int.Parse(route.Parameters[parameter.Name]));
                    
                }
                else
                {
                    parameters.Add(route.Parameters[parameter.Name]);
                }
            }

            return parameters.ToArray();
            //return action
            //    .GetParameters()
            //    .Select<ParameterInfo, object>(
            //        p =>
            //        {
            //            if (p.ParameterType == typeof(int))
            //            {
            //                return int.Parse(route._parameters[p.Name]);
            //            }

            //            return route._parameters[p.Name];
            //        }).ToArray();
        }
    }
}