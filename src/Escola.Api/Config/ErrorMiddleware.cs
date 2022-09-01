using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Exceptions;
using Escola.Domain.DTO;
using Newtonsoft.Json;

namespace Escola.Api.Config
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (Exception ex){
                await TratamentoExcecao(context, ex);
            }
        }

        private  Task TratamentoExcecao(HttpContext context, Exception ex)
        {
            HttpStatusCode status; 
            string message; 

            if (ex is DuplicadoException){
                status = HttpStatusCode.NotAcceptable;
                message = ex.Message;
            }
            else{
                status = HttpStatusCode.InternalServerError;
                message = "Ocorreu um erro favor contactar a TI";
            }

            var response = new ErrorDTO(message);

            context.Response.StatusCode = (int) status;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}