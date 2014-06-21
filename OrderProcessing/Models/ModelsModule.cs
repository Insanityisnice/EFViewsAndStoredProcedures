using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models
{
    public class ModelsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderProcessingContext>().To<OrderProcessingContext>();
        }
    }
}