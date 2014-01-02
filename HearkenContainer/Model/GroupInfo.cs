using System;
using System.Collections.Generic;
using HearkenContainer.Model.Collections;

namespace HearkenContainer.Model
{

    public class GroupInfo: IComparable
    {
        private string groupName;

        internal GroupInfo() { }

        public GroupInfo(string key)
        {
            this.Name = key;
            Triggers = new List<TriggerInfo>();
            Actions = new List<ActionInfo>();
        }

        /// <summary>
        /// Key, responsible for naming and finding the container group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<TriggerInfo> Triggers { get; set; }

        /// <summary>
        /// Listenters that will be executed as soon as necessary
        /// </summary>
        public IList<ActionInfo> Actions { get; set; }

        /// <summary>
        /// Interceptors, responsible for interfering in one or all actions
        /// </summary>
        public InterceptorList Interceptors { get; set; }

        public int CompareTo(object obj)
        {
            return (obj is string) ? this.Name.CompareTo(obj) :
                obj is GroupInfo ? this.Name.CompareTo(((GroupInfo)obj).Name) :
                -1;
        }

        
    }
}