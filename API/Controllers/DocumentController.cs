using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.UseCases.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentSource _source;

        public DocumentController(IDocumentSource documentSource)
        {
            _source = documentSource;
        }

        [HttpGet]
        [Route("document-queue")]
        public ActionResult GetDocumentInPdfQueue()
        {
            GetListOfDocuments find = new GetListOfDocuments(_source);
            var result=  find.Execute();
            return Ok(result.ValueOrFailure());
        }
    }
}
