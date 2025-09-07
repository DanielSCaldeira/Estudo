using Microsoft.AspNetCore.Builder;

namespace Estudo.API.Configuration
{
    public static class EndpointGroupHelper
    {
        // Cria um grupo de endpoints para um prefixo
        public static RouteGroupBuilder CreateGroup(WebApplication app, string prefix)
        {
            return app.MapGroup(prefix);
        }
    }
}
