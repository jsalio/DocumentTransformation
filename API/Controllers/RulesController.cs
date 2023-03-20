using Core.Contracts;
using Core.Models;
using Core.UseCases.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : BaseController, IRequest<ApplicationRules>
    {
        private readonly IRuleRepository _store;
        private ApplicationRules _rules;

        public RulesController(IRuleRepository ruleStore)
        {
            _store = ruleStore;
        }

        [HttpGet]
        [Route("current")]
        public ActionResult GetCurrentRules()
        {
            GetRules getRules = new GetRules(_store);
            var current = getRules.Execute();
            ValidateResult(current);
            return Ok(current.ValueOrFailure());
        }

        [HttpPost]
        [Route("save")]
        public ActionResult SaveChanges([FromBody] ApplicationRules rules)
        {
            _rules = rules;
            SaveChanges getRules = new SaveChanges(_store, this);
            var current = getRules.Execute();
            ValidateResult(current);
            return Ok();
        }

        ApplicationRules IRequest<ApplicationRules>.BuildRequest()
        => _rules;
    }
}
