﻿namespace NextGen.Cli
{
    public class Wrapper
    {
        public Wrapper(string name, string id)
        {
            Name = name;
            Id = id;
        }
        public string Name { get; }
        public string Id { get; }
    }
}
