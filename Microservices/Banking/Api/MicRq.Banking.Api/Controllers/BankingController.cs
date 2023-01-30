using Microsoft.AspNetCore.Mvc;
using MicRq.Banking.Application.Interface;
using MicRq.Banking.Application.Models;
using MicRq.Banking.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicRq.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private IAccountServices _accountServices;

        public BankingController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        // GET: api/<BankingController>
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return  Ok(_accountServices.GetAccounts());
        }

        // GET api/<BankingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BankingController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            _accountServices.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }

        // PUT api/<BankingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BankingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
