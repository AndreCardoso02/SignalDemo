using System;
using System.Collections.Generic;

namespace SignalRWithEntityFramework.Models;

public partial class HubConnection
{
    public int Id { get; set; }

    public string ConnectionId { get; set; }

    public string? Username { get; set; }
}
