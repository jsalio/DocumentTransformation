using Core.Contracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boundaries.Store.Repository
{
    public sealed class RuleRepository : IRuleRepository
    {
        private readonly IApplicationDbContext _context;

        public RuleRepository(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        Task<int> IRuleRepository.Create(ApplicationRules rules)
        {
            throw new NotImplementedException();
        }

        async Task<ApplicationRules> IRuleRepository.GetAll()
        {
            var query = await _context.Rules.ToArrayAsync();
            bool.TryParse(query.First(x => x.Name == "EnableLog").Value, out bool enableLog);
            bool.TryParse(query.First(x => x.Name == "LockFailsElements").Value, out bool lockFails);
            int.TryParse(query.First(x => x.Name == "TryLimits").Value, out int tryLimit);
            bool.TryParse(query.First(x => x.Name == "DeleteDocumentAfterSync").Value, out bool deleteAfterSync);
            bool.TryParse(query.First(x => x.Name == "ValidateBatchConverttion").Value, out bool validateConvertion);
            bool.TryParse(query.First(x => x.Name == "EnableConsole").Value, out bool enableConsole);
            bool.TryParse(query.First(x => x.Name == "EnableLocalConfig").Value, out bool useLocalconfig);

            var result = new ApplicationRules
            {
                EnableLog = enableLog,
                LockFailsElements = lockFails,
                TryLimits = tryLimit,
                DeleteDocumentAfterSync = deleteAfterSync,
                ValidateBatchConverttion = validateConvertion,
                EnableConsole = enableConsole,
                EnableLocalConfig = useLocalconfig
            };
            return result;
        }

        async Task<int> IRuleRepository.Update(IEnumerable<Rule> rules)
        {
            foreach (var rule in rules)
            {
                var target = _context.Rules.FirstOrDefault(x => x.Name == rule.Name);
                target.Value = rule.Value;
            }
            return await _context.SaveChanges();

        }
    }
}
