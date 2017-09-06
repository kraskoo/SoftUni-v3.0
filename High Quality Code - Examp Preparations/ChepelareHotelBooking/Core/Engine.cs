namespace HotelBookingSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Controllers;
    using Data;
    using Endpoints;
    using Interfaces;
    using Utilities;
    using Views;

    public class Engine : IEngine
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;
        private readonly Dictionary<string, Type> controllersByName;
        private readonly Dictionary<string, MethodInfo> methodsByName;

        public Engine(IInputReader reader, IOutputWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.controllersByName = new Dictionary<string, Type>();
            this.methodsByName = new Dictionary<string, MethodInfo>();
        }

        public void StartOperation()
        {
            var database = new HotelBookingSystemData();
            IUser currentUser = null;
            while (true)
            {
                string url = this.reader.ReadLine();
                if (string.IsNullOrEmpty(url))
                {
                    break;
                }

                var executionEndpoint = new Endpoint(url);
                if (!this.controllersByName.ContainsKey(executionEndpoint.ControllerName))
                {
                    var controllerInDict = AssemblyUtilities.Types
                        .FirstOrDefault(type => type.Name == executionEndpoint.ControllerName);
                    if (controllerInDict != null)
                    {
                        this.controllersByName.Add(
                            executionEndpoint.ControllerName,
                            controllerInDict);
                    }
                }

                var controllerType = this.controllersByName[executionEndpoint.ControllerName];
                var controller = Activator.CreateInstance(
                    controllerType,
                    database,
                    currentUser,
                    executionEndpoint.ActionName) as Controller;
                var methodName = $"{executionEndpoint.ControllerName}{executionEndpoint.ActionName}";
                if (!this.methodsByName.ContainsKey(methodName))
                {
                    this.methodsByName.Add(
                        methodName,
                        controllerType.GetMethod(executionEndpoint.ActionName));
                }

                var action = this.methodsByName[methodName];
                object[] parameters = MapParameters(executionEndpoint, action);
                string viewResult = string.Empty;
                try
                {
                    var view = action.Invoke(controller, parameters) as IView;
                    viewResult = view.Display();
                    currentUser = controller.CurrentUser;
                }
                catch (Exception ex)
                {
                    viewResult = new ErrorView(ex.InnerException.Message).Display();
                }

                this.writer.WriteLine(viewResult);
            }
        }

        private static object[] MapParameters(IEndpoint executionEndpoint, MethodInfo action)
        {
            var parameters = action
                .GetParameters()
                .Select<ParameterInfo, object>(p =>
                {
                    if (p.ParameterType == typeof(int))
                    {
                        return int.Parse(executionEndpoint.Parameters[p.Name]);
                    }

                    if (p.ParameterType == typeof(decimal))
                    {
                        return decimal.Parse(executionEndpoint.Parameters[p.Name]);
                    }

                    if (p.ParameterType == typeof(DateTime))
                    {
                        return DateTime.ParseExact(
                            executionEndpoint.Parameters[p.Name],
                            Constants.DateFormat,
                            CultureInfo.InvariantCulture);
                    }

                    return executionEndpoint.Parameters[p.Name];
                })
               .ToArray();

            return parameters;
        }
    }
}