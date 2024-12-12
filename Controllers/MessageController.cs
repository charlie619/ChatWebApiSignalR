using ChatWebApiSignalR.DataAccess;
using ChatWebApiSignalR.DTO;
using ChatWebApiSignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// This is an API controller for handling messages.
[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly AppDbContext _dbContext;

    // Injecting the SignalR hub context to send messages from this API controller.
    public MessageController(IHubContext<ChatHub> hubContext, AppDbContext dbContext)
    {
        _hubContext = hubContext;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var messages = await _dbContext.ChatMessages
            .OrderByDescending(m => m.Timestamp)
            .ToListAsync();
        return Ok(messages);
    }

    // POST api/message
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ChatMessage request)
    {//DB Server ===> 100 Records

        // Save the message to the database
        _dbContext.ChatMessages.Add(request);
        await _dbContext.SaveChangesAsync();

        // Sending the message to all connected clients via SignalR Hub.
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.User, request.Message);
        return Ok(new {Status = "Message Sent", MessageId = request.Id});
    }
}
