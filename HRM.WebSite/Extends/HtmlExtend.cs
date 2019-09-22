﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HRM.WebSite.Extends
{
    public static class HtmlExtend
    {
        private static string GetPropertyName<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            // Get field name
            // Code from: https://stackoverflow.com/a/2916344/4794469
            MemberExpression body = expression.Body as MemberExpression;
            if (body != null) return body.Member.Name;

            UnaryExpression ubody = expression.Body as UnaryExpression;
            if (ubody != null) body = ubody.Operand as MemberExpression;

            return body?.Member.Name;
        }

        public static MvcHtmlString SetClassOnErrorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string @class)
        {
            var propertyName = GetPropertyName(expression);
            if (propertyName == null)
                return MvcHtmlString.Empty;

            if (htmlHelper.ViewData.ModelState.IsValid || !htmlHelper.ViewData.ModelState.ContainsKey(propertyName))
                return MvcHtmlString.Empty;

            return htmlHelper.ViewData.ModelState[propertyName].Errors.Any()
                ? MvcHtmlString.Create(@class)
                : MvcHtmlString.Empty;
        }
    }
}