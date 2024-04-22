namespace DnDCharacterSheet.Web.Infrastructure;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoint(
        this IEndpointRouteBuilder builder,
        Func<IEndpointRouteBuilder, string, Delegate, IEndpointConventionBuilder> mapMethod,
        Delegate handler,
        string pattern = "",
        params int[] statusCodes)
    {
        Guard.Against.AnonymousMethod(handler);

        var endpointBuilder = mapMethod(builder, pattern, handler).WithName(handler.Method.Name);
        
        var routeBuilder = endpointBuilder as RouteHandlerBuilder;

        if (routeBuilder is null) throw new Exception("Unexpected error while building endpoints");

        foreach (var statusCode in statusCodes)
        {
            routeBuilder.Produces(statusCode.Code).WithDescription(statusCode.Summary);
        }

        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoint<T>(
        this IEndpointRouteBuilder builder,
        Func<IEndpointRouteBuilder, string, Delegate, IEndpointConventionBuilder> mapMethod,
        Delegate handler,
        string pattern = "",
        params int[] statusCodes)
    {
        Guard.Against.AnonymousMethod(handler);

        var endpointBuilder = mapMethod(builder, pattern, handler).WithName(handler.Method.Name);

        var routeBuilder = endpointBuilder as RouteHandlerBuilder;

        if (routeBuilder is null) throw new Exception("Unexpected error while building endpoints");

        foreach (var statusCode in statusCodes)
        {
            if (statusCode.Code == 200 || statusCode.Code == 201)
            {
                routeBuilder.Produces<T>(statusCode.Code).WithDescription(statusCode.Summary);
                continue;
            }
            routeBuilder.Produces(statusCode.Code).WithDescription(statusCode.Summary);
        }

        return builder;
    }

    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint(EndpointRouteBuilderExtensions.MapGet, handler, pattern, statusCodes);
    
    public static IEndpointRouteBuilder MapGet<T>(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint<T>(EndpointRouteBuilderExtensions.MapGet, handler, pattern, statusCodes);

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint(EndpointRouteBuilderExtensions.MapPost, handler, pattern, statusCodes);

    public static IEndpointRouteBuilder MapPost<T>(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint<T>(EndpointRouteBuilderExtensions.MapPost, handler, pattern, statusCodes);

    public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint(EndpointRouteBuilderExtensions.MapPut, handler, pattern, statusCodes);

    public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "", 
        params int[] statusCodes) =>
        builder.MapEndpoint(EndpointRouteBuilderExtensions.MapDelete, handler, pattern, statusCodes);
}
