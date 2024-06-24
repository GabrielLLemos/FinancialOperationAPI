using FinancialOperationAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System;
using FinancialOperationAPI.Controllers.Requests;
using FinancialOperationAPI.Repositories;

namespace FinancialOperationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IList<Conta>> Get([FromServices] IContaRepository contaRepository)
        {
            var contas = contaRepository.GetAll().ToList();

            return Ok(contas);
        }

        [HttpGet("{id}")]
        public ActionResult<Conta> Get(
            Guid id,
            [FromServices] IContaRepository contaRepository
         )
        {
            var conta = contaRepository.GetById(id);

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

        [HttpPost]
        public ActionResult<Conta> Post(
            [FromBody] ContaRequest request,
            [FromServices] IContaRepository contaRepository
            )
        {
            var conta = new Conta();

            conta.SetNome(request.Nome);
            conta.SetSaldo(request.Saldo);

            contaRepository.Add(conta);

            return CreatedAtAction(nameof(Get), new { id = conta.Id }, conta);
        }

        [HttpPut("{id}")]
        public ActionResult Put(
            Guid id, 
            string nome,
            [FromServices] IContaRepository contaRepository
            )
        {
            contaRepository.Update(id, nome);
            /*var existingConta = _contas.FirstOrDefault(c => c.Id == id);

            if (existingConta == null)
                return NotFound();

            existingConta.SetNome(nome);
            */
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(
            Guid id,
            [FromServices] IContaRepository contaRepository
            )
        {
            contaRepository.Delete(id);
            /*
            var conta = _contas.FirstOrDefault(c => c.Id == id);

            if (conta == null)
                return NotFound();

            _contas.Remove(conta);
            */
            return NoContent();
        }

        // Depositar dinheiro na conta
        [HttpPost("{id}/depositar")]
        public ActionResult<Conta> Depositar(
            Guid id,
            [FromBody] double valor,
            [FromServices] IContaRepository contaRepository
        )
        {
            var conta = contaRepository.GetById(id);

            if (conta == null)
            {
                return NotFound();
            }

            var novoSaldo = conta.Saldo + valor;

            conta = contaRepository.Update(id, null, novoSaldo);
            /*
            if (existingConta == null)
                return NotFound();

            existingConta.SetSaldo(existingConta.Saldo + saldo);
            */
            return Ok(conta);
        }

        // Sacar
        [HttpPut("{id}/saque")]
        public ActionResult<Conta> Sacar(
            Guid id,
            [FromBody] double saque,
            [FromServices] IContaRepository contaRepository
        )
        {
            var conta = contaRepository.GetById(id);

            if (conta == null)
            {
                return NotFound();
            }

            if (saque > conta.Saldo)
            {
                return BadRequest();
            }

            var novoSaldo = conta.Saldo - saque;

            conta = contaRepository.Update(id, null, novoSaldo);

            return Ok(conta);
        }

        // Ver Saldo
        [HttpGet("{id}/saldo")]
        public ActionResult<Conta> ExibirSaldo(
            Guid id,
            [FromServices] IContaRepository contaRepository
        )
        {
            
            var existingConta = contaRepository.GetById(id);

            if (existingConta == null)
                return NotFound();
            
            return Ok(existingConta);
        }
    }
}