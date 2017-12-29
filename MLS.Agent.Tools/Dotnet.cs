﻿using System;
using System.IO;

namespace MLS.Agent.Tools
{
    public class Dotnet
    {
        private readonly TimeSpan _defaultCommandTimeout;
        private readonly DirectoryInfo _workingDirectory;

        public Dotnet(
            DirectoryInfo workingDirectory = null,
            TimeSpan? defaultCommandTimeout = null)
        {
            _defaultCommandTimeout = defaultCommandTimeout ??
                                     TimeSpan.FromSeconds(10);
            _workingDirectory = workingDirectory ??
                                new DirectoryInfo(Directory.GetCurrentDirectory());
        }

        public CommandLineResult New(string templateName, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(templateName));
            }

            return Execute($"new {templateName}", timeout);
        }

        public CommandLineResult Build(TimeSpan? timeout = null) =>
            Execute("build", timeout);

        public CommandLineResult Restore(TimeSpan? timeout = null) =>
            Execute("restore", timeout);

        public CommandLineResult Run(TimeSpan? timeout = null) =>
            Execute("run", timeout);

        public CommandLineResult Execute(string args, TimeSpan? timeout = null)
        {
            timeout = timeout ?? _defaultCommandTimeout;

            return CommandLine.Execute(
                DotnetMuxer.Path,
                args,
                _workingDirectory,
                timeout);
        }
    }
}
