﻿using DocumentFormat.OpenXml.Drawing.Diagrams;
using Petsi.Interfaces;
using Petsi.Utils;
using System.Collections.Generic;

namespace Petsi.Services
{
    /// <summary>
    /// When Initializing the POMT on a new machine, the startup filepath is utilized to give various models
    /// and services files to jumpstart the information building process.
    /// </summary>
    public class StartupService : IStartupRegistry
    {
        private static StartupService instance;
        private string startupFp;
        private List<IStartupSubscriber> subscribers;
        private List<(string fileName, string filePath)> FileList;
        public static StartupService Instance
        {
            get 
            { 
                if (instance == null) 
                {
                    instance = new StartupService();
                }
                return instance; 
            } 
        }

        private StartupService() 
        {
            startupFp = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_STARTUP);
            subscribers = new List<IStartupSubscriber>();
        }

        public void Start()
        {
            if (!IsDirectoryEmpty(startupFp))
            {
                DirectoryInfo d = new DirectoryInfo(startupFp);
                FileList = new List<(string fileName, string filePath)>();
                foreach (var file in d.GetFiles())
                {
                    FileList.Add((file.Name, file.FullName));
                }
                Notify();
            } 
        }

        public bool IsDirectoryEmpty(string path)
        {
            if(path == null) { return false; }
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        /// <summary>
        /// The order that objects subscribe and when startup service is activated may be out of order, so when somone subscribes, the should check for their relevant files immiediately.
        /// </summary>
        /// <param name="subscriber"></param>
        public void Register(IStartupSubscriber subscriber)
        {
            subscribers.Add(subscriber);
            Notify();
        }

        public void Deregister(IStartupSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        public void Notify()
        {
            foreach(var subscriber in subscribers)
            {
                subscriber.Update(FileList);
            }
        }
    }
}
