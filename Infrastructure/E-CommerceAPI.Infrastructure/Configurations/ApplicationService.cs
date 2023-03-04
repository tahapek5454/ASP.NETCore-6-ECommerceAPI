using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.DTOs.Configurations;
using E_CommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitonEndPoints(Type type)
        {
            //verilen typın assmplysini bulalım
            //controller base sınıfından turuyen tum metodları oncelikle bi alalım
            Assembly? assembly = Assembly.GetAssembly(type);
            var controllers = assembly?.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> menus = new List<Menu>();

            // iligli controllerları aldik simdi bu controllerdaki attirbutlardan bize lazımolacaklar çekelim
            // bu olay reflection

            if(controllers != null)
            {
                foreach (var controller in controllers)
                {
                    // controllerdan bizim tanımladıgımız attributtan olanların metodlarını aldık
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if(actions != null)
                    {
                        foreach (var action in actions)
                        {
                            // burda actionan tum attributelarını çektik
                            var attributes = action.GetCustomAttributes(true);

                            // simdi sahip oldugu attributlardan bize lazım olanları çekelim
                            if(attributes != null)
                            {
                                Menu menu = null;

                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                
                                if(!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new Menu() { Name = authorizeDefinitionAttribute.Menu};
                                    menus.Add(menu);
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                                }

                                Application.DTOs.Configurations.Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionTypes), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };

                                // HttpAttribute alalım
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                                if(httpAttribute != null)
                                {
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                }
                                else
                                {
                                    _action.HttpType = HttpMethods.Get;
                                }


                                //ozellestirme
                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                                // menunu actiona eklemek kaldı
                                menu.Actions.Add(_action);
                               
                            }
                        }

                    }

                }
            }

            return menus;
        }
    }
}
