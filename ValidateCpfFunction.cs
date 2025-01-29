using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace validacpf
{
    public static class ValidateCpfFunction
    {
        [FunctionName("ValidateCpfFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string cpf = data?.cpf;

            if (IsValidCpf(cpf))
            {
                return new OkObjectResult($"CPF {cpf} é válido.");
            }
            else
            {
                return new BadRequestObjectResult($"CPF {cpf} é inválido.");
            }
        }
 
    private static bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11 || !long.TryParse(cpf, out _))
            {
                return false;
            }

            // Elimina CPFs inválidos conhecidos
            string[] invalidCpfs = {
        "00000000000", "11111111111", "22222222222",
        "33333333333", "44444444444", "55555555555",
        "66666666666", "77777777777", "88888888888", "99999999999"
    };

            if (Array.Exists(invalidCpfs, element => element == cpf))
            {
                return false;
            }

            // Validação do primeiro dígito
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (cpf[i] - '0') * (10 - i);
            }

            int remainder = sum % 11;
            int firstCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            if (cpf[9] - '0' != firstCheckDigit)
            {
                return false;
            }

            // Validação do segundo dígito
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (cpf[i] - '0') * (11 - i);
            }

            remainder = sum % 11;
            int secondCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            if (cpf[10] - '0' != secondCheckDigit)
            {
                return false;
            }

            return true;
        }

    }
}
