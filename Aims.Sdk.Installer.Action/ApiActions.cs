using System;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using WixToolset.Dtf.WindowsInstaller;
using Env = System.Environment;

namespace Aims.Sdk.Installer.Actions
{
    public class ApiActions
    {
        [CustomAction]
        public static ActionResult DeleteToken(Session session)
        {
            try
            {
                session.Log("Begin DeleteToken");

                Api api = GetApi(session);
                try
                {
                    api.Auth.DeleteToken(session["AIMS_API_TOKEN"]);
                    session.Log("DeleteToken, made successful request");
                    session["AIMS_API_ERROR"] = "";
                }
                catch (Exception ex)
                {
                    session["AIMS_API_ERROR"] = ex.Message;
                    session.Log("DeleteToken, request failed: {0}", ex.Message);
                    return ActionResult.Failure;
                }
                session.Log("End DeleteToken");

                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("DeleteToken, exception: {0}", ex.Message);
                return ActionResult.Failure;
            }
        }

        [CustomAction]
        public static ActionResult GetToken(Session session)
        {
            try
            {
                session.Log("Begin GetToken");

                Api api = GetApi(session);
                try
                {
                    var system = JsonConvert.DeserializeObject<System>(session["AIMS_SYSTEM"]);
                    if (system.Id == 0)
                    {
                        system = CreateSystem(session, api);
                    }
                    session["AIMS_API_TOKEN"] = api.Auth.GetAgentToken(system);
                    session.Log("GetToken, made successful request");
                    session["AIMS_API_ERROR"] = "";
                }
                catch (Exception ex)
                {
                    session["AIMS_API_ERROR"] = ex.Message;
                    session.Log("GetToken, request failed: {0}", ex.Message);
                    return ActionResult.Failure;
                }
                session.Log("End GetToken");

                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("GetToken, exception: {0}", ex.Message);
                return ActionResult.Failure;
            }
        }

        [CustomAction]
        public static ActionResult ReadEnvironments(Session session)
        {
            try
            {
                session.Log("Begin ReadEnvironments");

                Api api = GetApi(session);
                try
                {
                    Environment[] environments = api.Environments.Get();
                    session.Log("ReadEnvironments, made successful request");

                    ClearListBox(session, "AIMS_ENVIRONMENT");
                    session["AIMS_ENVIRONMENT"] = "";

                    int i = 1;
                    foreach (Environment e in environments)
                    {
                        AddListBoxItem(session, "AIMS_ENVIRONMENT", i++, e.DisplayName,
                            JsonConvert.SerializeObject(e));
                    }

                    session["AIMS_API_ERROR"] = "";
                }
                catch (Exception ex)
                {
                    session["AIMS_API_ERROR"] = ex.Message;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    session.Log("ReadEnvironments, request failed: {0}", ex.Message);
                }
                session.Log("End ReadEnvironments");

                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("ReadEnvironments, exception: {0}", ex.Message);
                return ActionResult.Failure;
            }
        }

        [CustomAction]
        public static ActionResult ReadSystems(Session session)
        {
            try
            {
                session.Log("Begin ReadSystems");

                Api api = GetApi(session);
                try
                {
                    string agentId = session["AIMS_AGENT_ID"];
                    System[] systems = new[] { new System { Name = "Create a new system" } }
                        .Concat(api
                            .ForEnvironment(Guid.Parse(session["AIMS_ENVIRONMENT_ID"]))
                            .Systems.Get()
                            .Where(s => s.AgentId == agentId))
                        .ToArray();
                    session.Log("ReadSystems, made successful request");

                    ClearListBox(session, "AIMS_SYSTEM");
                    session["AIMS_SYSTEM"] = "";

                    int i = 1;
                    foreach (System s in systems)
                    {
                        AddListBoxItem(session, "AIMS_SYSTEM", i++, s.Name,
                            JsonConvert.SerializeObject(s));
                    }

                    session["AIMS_API_ERROR"] = "";
                }
                catch (Exception ex)
                {
                    session["AIMS_API_ERROR"] = ex.Message;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    session.Log("ReadSystems, request failed: {0}", ex.Message);
                }
                session.Log("End ReadSystems");

                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("ReadSystems, exception: {0}", ex.Message);
                return ActionResult.Failure;
            }
        }

        private static void AddListBoxItem(Session session, string propertyName, int index, string text, string value)
        {
            using (var view = session.Database.OpenView(session.Database.Tables["ListBox"].SqlSelectString))
            {
                view.Execute();
                view.Modify(ViewModifyMode.InsertTemporary, new Record(new object[] { propertyName, index, value, text }));
            }
        }

        private static void ClearListBox(Session session, string propertyName)
        {
            using (var view = session.Database
                .OpenView(String.Format("DELETE FROM ListBox WHERE ListBox.Property='{0}'", propertyName)))
            {
                view.Execute();
            }
        }

        private static System CreateSystem(Session session, Api api)
        {
            Guid environmentId = Guid.Parse(session["AIMS_ENVIRONMENT_ID"]);
            var system = new System
            {
                Name = Env.MachineName,
                AgentId = session["AIMS_AGENT_ID"],
                Version = session["AIMS_AGENT_VERSION"],
                EnvironmentId = environmentId,
            };
            return api.ForEnvironment(environmentId)
                .Systems.Add(system);
        }

        private static Api GetApi(Session session)
        {
            return new Api(new Uri(new Uri(session["AIMS_API_ENDPOINT"]), "api/"),
                new HttpBasicCredentials(session["AIMS_EMAIL"], session["AIMS_PASSWORD"]));
        }
    }
}