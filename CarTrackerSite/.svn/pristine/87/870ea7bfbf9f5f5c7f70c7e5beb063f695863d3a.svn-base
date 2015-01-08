using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace WebSite.Core.MvcSiteMap
{
    public class RoleVisibilityProvider : ISiteMapNodeVisibilityProvider
    {
        public bool IsVisible(SiteMapNode node, HttpContext context, IDictionary<string, object> sourceMetadata)
        {
            // Convert to MvcSiteMapNode
            var mvcNode = node as MvcSiteMapNode;
            if (mvcNode == null)
                return true;

            // Is visibility attribute specified?
            string visibility = mvcNode["visibility"];

            if (string.IsNullOrEmpty(visibility))
                return true;

            visibility = visibility.Trim();

            //Process visibility
            switch (visibility)
            {
                case "Auth":

                    return context.Request.IsAuthenticated;

                case "!Auth":

                    return !context.Request.IsAuthenticated;

                default:

                    return context.Request.IsAuthenticated && Roles.GetRolesForUser().Intersect(visibility.Split(',')).Count() > 0;
            }
        }
    }
}