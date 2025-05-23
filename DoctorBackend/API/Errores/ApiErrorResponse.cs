﻿
namespace API.Errores
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeSatusCode(statusCode);
        }

        private string GetMensajeSatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Se ha realizado una solicitud no valida",
                401 => "No estas autorizado para este recurso",
                404 => "Recurso no encontrado",
                500 => "Error interno del Servidor",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }
    }
}
