namespace Estudo.API.Configuration
{
    public static class EndpointAuthorizationExtensions
    {
        // Centraliza o registro dos endpoints protegidos
        public static void RegisterProtectedEndpoints(WebApplication app)
        {
            var configuration = app.Configuration;
            // Grupo de clientes protegido
            var clientes = app.MapGroup("/clientes").RequireAuthorization().WithTags("Clientes");
            ClienteController.RegisterEndpoints(clientes);

            // Grupo de pedidos protegido
            var pedidos = app.MapGroup("/pedidos").RequireAuthorization().WithTags("Pedidos");
            PedidoController.RegisterEndpoints(pedidos);

            // Grupo de autenticação (sem RequireAuthorization)
            var auth = app.MapGroup("/auth").WithTags("Autenticação");
            AutenticarController.RegisterEndpoints(auth, configuration);
        }
    }
}
/*
Este arquivo centraliza a aplicação de autorização nos grupos de endpoints.
Os controllers recebem o grupo como parâmetro e registram os endpoints nele.
*/
