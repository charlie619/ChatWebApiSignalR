using ChatWebApiSignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

// This is an API controller for handling messages.
[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    // Injecting the SignalR hub context to send messages from this API controller.
    public MessageController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    // POST api/message
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageRequest request)
    {//DB Server ===> 100 Records
        // Sending the message to all connected clients via SignalR Hub.
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.User, request.Message);
        return Ok();
    }
}

// Define a simple DTO (Data Transfer Object) to accept user and message data.
public class MessageRequest
{
    public string User { get; set; }
    public string Message { get; set; }
}
