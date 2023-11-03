using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;
using GVPB.Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace GVPB.Identity.Api.UseCases
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test : ControllerBase
    {
        private readonly LanguageManager<SharedResources> languageService;
        public Test(LanguageManager<SharedResources> languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet]
        [Route("test")]
        public IActionResult test()
        {
            var user = new User(languageService){Id = Guid.Empty, UserName = "", Email = "", Password = "", Rule = Domain.Enum.Rules.USER};
            var test =user.IsValid;
            return Ok(user.ValidationResult.ToString());
        }
    } 
}