namespace CustomerManagementSystem.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                context.Response.StatusCode = 500; // Internal Server Error
                context.Response.ContentType = "application/json";
                var errorResponse = new { Message = "An unexpected error occurred. Please try again later." };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
