using Juke_Mobile_Gui.Controllers;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Juke_Mobile_Gui.Helper
{
    public class RavenDbApiAttribute : ActionFilterAttribute
    {
        readonly IDocumentStore documentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenDbApiAttribute" /> class.
        /// </summary>
        /// <param name="documentStore">The document store.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public RavenDbApiAttribute(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");
            this.documentStore = documentStore;
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as RavenController;
            if (controller == null)
                return;

            controller.Db = documentStore.OpenSession();
        }

        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as RavenController;
            if (controller == null)
                return;

            controller.Db.Dispose();
        }
    }
}
