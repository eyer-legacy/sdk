using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;
using WixToolset.Dtf.WindowsInstaller;
using Env = System.Environment;

namespace Aims.Sdk.Installer.Actions
{
    public class ServiceActions
    {
        [CustomAction]
        public static ActionResult ValidateServiceAccount(Session session)
        {
            session.Log("Begin ValidateServiceAccount");

            try
            {
                string account = session["AIMS_SERVICE_USER"];
                if (!account.Contains(@"\"))
                {
                    session["AIMS_SERVICE_USER"] = account = @".\" + account;
                }
                string[] parts = account.Split('\\');
                if (parts.Length > 2)
                {
                    session["AIMS_SERVICE_ACCOUNT_VALID"] = "0";
                    session.Log("End ValidateServiceAccount");
                    return ActionResult.Success;
                }
                using (var pc = parts[0] == "."
                    || parts[0].ToUpperInvariant() == Env.MachineName
                    ? new PrincipalContext(ContextType.Machine)
                    : new PrincipalContext(ContextType.Domain, parts[0]))
                {
                    if (pc.ValidateCredentials(parts.Last(), session["AIMS_SERVICE_PASSWORD"]))
                    {
                        session["AIMS_SERVICE_ACCOUNT_VALID"] = "1";
                    }
                    else
                    {
                        session["AIMS_SERVICE_ACCOUNT_VALID"] = "0";
                        MessageBox.Show("Incorrect username or password.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            session.Log("End ValidateServiceAccount");
            return ActionResult.Success;
        }
    }
}