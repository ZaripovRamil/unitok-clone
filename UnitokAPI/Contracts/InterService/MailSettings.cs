﻿namespace Contracts.InterService;

public class MailSettings
{
    public string Host { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; } 
    public int Port { get; set; }
    public bool EnableSsl { get; set; } 
}