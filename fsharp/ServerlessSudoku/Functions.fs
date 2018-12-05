module Functions

open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.Http
open Microsoft.AspNetCore.Http

[<FunctionName("Solve")>]
let solve ([<HttpTrigger(AuthorizationLevel.Anonymous, "post", Route="solve")>] req: HttpRequest) =
    Handlers.solve req