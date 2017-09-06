namespace HotelBookingSystem.Controllers
{
    using System;
    using System.Linq;
    using Enums;
    using Exceptions;
    using Interfaces;
    using Utilities;
    using Views;

    public abstract class Controller : IController
    {
        protected Controller(IHotelBookingSystemData data, IUser user, string methodName)
        {
            this.Data = data;
            this.CurrentUser = user;
            this.MethodName = methodName;
        }

        public string MethodName { get; }

        public IUser CurrentUser { get; protected set; }

        public bool HasCurrentUser => this.CurrentUser != null;

        protected IHotelBookingSystemData Data { get; private set; }

        protected IView View(object model)
        {
            string fullNamespace = this.GetType().Namespace;
            int firstSeparatorIndex = fullNamespace
                .IndexOf(
                    Constants.NamespaceSeparator,
                    StringComparison.Ordinal);
            string baseNamespace = fullNamespace.Substring(0, firstSeparatorIndex);
            string controllerName = this.GetType().Name.Replace(Constants.ControllerSuffix, string.Empty);
            string actionName = this.MethodName;
            string fullPath = string.Join(
                Constants.NamespaceSeparator,
                baseNamespace,
                Constants.ViewsFolder,
                controllerName,
                actionName);
            var viewType = AssemblyUtilities.CurrentAssembly
                .GetType(fullPath);
            return Activator.CreateInstance(viewType, model) as IView;
        }

        protected IView NotFound(string message)
        {
            return new ErrorView(message);
        }

        protected void Authorize(params Role[] roles)
        {
            if (!this.HasCurrentUser)
            {
                throw new ArgumentException("There is no currently logged in user.");
            }

            if (!roles.Any(role => this.CurrentUser.IsInRole(role)))
            {
                throw new AuthorizationFailedException(
                    "The currently logged in user doesn't have sufficient rights to perform this operation.");
            }
        }
    }
}