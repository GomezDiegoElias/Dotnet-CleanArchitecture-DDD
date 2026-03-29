using Mapster;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;

namespace BuberDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public AuthenticationMappingConfig()
    {
    }

    public void Register(TypeAdapterConfig config)
    {
        // Aunque estos dos son redundantes, esta bueno porque estamos al tanto de los diversos mapeos 
        // y tambien si alguien necesita configurar algo especial, sepa exactamente donde hacerlo
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
          .Map(dest => dest, src => src.User);
    }
}
