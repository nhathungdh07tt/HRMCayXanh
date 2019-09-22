using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace HRM.WebSite.Bootstrap
{
    public static class IocConfig
    {
        private static ContainerBuilder _containerBuilder;
        private static readonly object @lock = new object();

        public static ContainerBuilder ContainerBuilder
        {
            get
            {
                lock (@lock)
                {
                    return _containerBuilder ?? (_containerBuilder = new Autofac.ContainerBuilder());
                }
            }
        }
    }
}