using System.Globalization;
using Newtonsoft.Json;
using WixToolset.Dtf.WindowsInstaller;

namespace Aims.Sdk.Installer.Actions
{
    public class UiActions
    {
        [CustomAction]
        public static ActionResult SaveEnvironment(Session session)
        {
            try
            {
                var environment = JsonConvert.DeserializeObject<Environment>(session["AIMS_ENVIRONMENT"]);
                session["AIMS_ENVIRONMENT_ID"] = environment.Id.ToString();
                session["AIMS_ENVIRONMENT_NAME"] = environment.DisplayName;

                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Failure;
            }
        }

        [CustomAction]
        public static ActionResult SaveSystem(Session session)
        {
            try
            {
                var system = JsonConvert.DeserializeObject<System>(session["AIMS_SYSTEM"]);
                session["AIMS_SYSTEM_ID"] = system.Id.ToString(CultureInfo.InvariantCulture);
                session["AIMS_SYSTEM_NAME"] = system.Name;

                return ActionResult.Success;
            }
            catch
            {
                return ActionResult.Failure;
            }
        }
    }
}