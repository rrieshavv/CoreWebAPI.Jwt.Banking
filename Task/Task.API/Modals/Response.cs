﻿namespace Task.API.Modals
{
    public class Response
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; }
    }
}
