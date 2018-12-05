module Functions

open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.Http
open Microsoft.AspNetCore.Http

[<FunctionName("LcdDisplay")>]
let display ([<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="lcd-display")>] req: HttpRequest) =
    Handlers.display req