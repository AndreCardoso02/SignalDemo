using System;
using System.Collections.Generic;

namespace SignalRWithEntityFramework.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Message { get; set; }

    public string MessageType { get; set; } = null!;

    public DateTime NotificationDateTime { get; set; }
}
