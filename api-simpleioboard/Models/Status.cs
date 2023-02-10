﻿namespace api_simpleioboard.Models
{
    public class Status
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public bool Enabled { get; set; } = true;
    }
}
