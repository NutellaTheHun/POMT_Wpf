﻿using Petsi.Filing;
using Petsi.Interfaces;

namespace Petsi.Managers
{
    /// <summary>
    /// When a report is made, the environment is captured and associated with the report Id for debriefing/debugging when needed.
    /// </summary>
    public class EnvironCaptureRegistrySingleton : IEnvironRegistry, IEnvironCapturable
    {
        private static EnvironCaptureRegistrySingleton _instance;
        private List<IEnvironCapturable> _environments;
        private EnvironCaptureRegistrySingleton() { _environments = new List<IEnvironCapturable>(); }
        public static EnvironCaptureRegistrySingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EnvironCaptureRegistrySingleton();
            }
            return _instance;
        }

        public void Register(IEnvironCapturable component)
        {
            _environments.Add(component);
        }
        public void Deregister(IEnvironCapturable component)
        {
            _environments.Remove(component);
        }

        public void CaptureEnvironment(FileBehavior reportFb)
        {
            foreach(IEnvironCapturable env in _environments)
            {
                env.CaptureEnvironment(reportFb);
            }
        }
    }
}
