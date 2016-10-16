using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Api.Models
{
    public class UserModelBinder : IModelBinder
    {
        public UserModelBinder()
        {

        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            try
            {
                dynamic jsonParameters = ParseJsonObjectAndReturnAsDynamic(actionContext);
                User parsedUser = ParseUserFromJsonParameters(jsonParameters);
                AddIDToParsedUserIfNeeded(actionContext, jsonParameters, parsedUser);

                bindingContext.Model = parsedUser;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private dynamic ParseJsonObjectAndReturnAsDynamic(HttpActionContext actionContext)
        {
            Task<string> content = actionContext.Request.Content.ReadAsStringAsync();
            string body = content.Result;
            return JObject.Parse(body);
        }

        private User ParseUserFromJsonParameters(dynamic jsonParameters)
        {
            string userName = jsonParameters.Name;
            string userEmail = jsonParameters.Email;
            string userPassword = jsonParameters.Password;
            JArray rolesJsonArray = (JArray)jsonParameters.Roles;

            ICollection<Role> processedRoles = FillRolesListUsingClassesNamesInJson(rolesJsonArray);
            return new User(userName, userEmail, userPassword, processedRoles);
        }

        private ICollection<Role> FillRolesListUsingClassesNamesInJson(JArray rolesJsonArray)
        {
            ICollection<Role> processedRoles = new List<Role>();

            foreach (string oneRoleName in rolesJsonArray)
            {
                Type roleType = Type.GetType("TodoPagos.UserAPI." + oneRoleName + ",UserAPI");
                ConstructorInfo roleConstructor = roleType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
                dynamic roleInstance = roleConstructor.Invoke(null);
                processedRoles.Add(roleInstance);
            }

            return processedRoles;
        }

        private void AddIDToParsedUserIfNeeded(HttpActionContext actionContext, dynamic jsonParameters, User parsedUser)
        {
            if (actionContext.Request.Method == HttpMethod.Put
                    || actionContext.Request.Method == HttpMethod.Delete)
            {
                int id = int.Parse(jsonParameters["Id"]);
                parsedUser.ID = id;
            }
        }
    }
}