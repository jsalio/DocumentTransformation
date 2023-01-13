using Core.Contracts;
using Core.Models;
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
            Core.UseCase.Rules.GetRules getRules = new Core.UseCase.Rules.GetRules(_store);
            var current = getRules.Execute();
            ValidateResult(current);
            return Ok(current.ValueOrFailure());
        }

        [HttpPost]
        [Route("save")]
        public ActionResult SaveChanges([FromBody] ApplicationRules rules)
        {
            _rules = rules;
            Core.UseCase.Rules.SaveChanges getRules = new Core.UseCase.Rules.SaveChanges(_store, this);
            var current = getRules.Execute();
            ValidateResult(current);
            return Ok();
        }

        ApplicationRules IRequest<ApplicationRules>.BuildRequest()
        => _rules;
    }
}
