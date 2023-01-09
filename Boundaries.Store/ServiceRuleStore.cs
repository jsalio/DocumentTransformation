using Core.Contracts;
using Core.Models;

namespace Boundaries.Store
{
    public sealed class ServiceRuleStore : IServiceRule
    {
        ApplicationRules IServiceRule.GetRules()
        {
            return new ApplicationRules
                {
                    DeleteDocumentAfterSync = true,
                    ValidateBatchConverttion = true,
                    EnableConsole = true,
                    EnableLocalConfig = false,
                    EnableLog = true,
                    LockFailsElements = true,
                    TryLimits = 3
                };
        }

        string IServiceRule.Save(ApplicationRules queue)
        {
            return "Ok";
        }
    }
}
