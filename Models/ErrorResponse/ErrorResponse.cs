using System.Net;

namespace CrudPatrimonioEmpresarialJWT.Models.ErrorResponse
{
    public class ErrorResponse
    {
        private const string ERROR_MESSAGE = "Algo deu errado, verifique e tente novamente.";

        public string Message { get; } = ERROR_MESSAGE;
        public HttpStatusCode StatusCode { get; set; }
    }
}
