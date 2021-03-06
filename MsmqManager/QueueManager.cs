﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public class QueueManager
    {
        private List<MessageQueue> _queues = null;
        public int QueuesCount { get { return _queues.Count; } }
        public QueueManager()
        {
            _queues = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName).ToList();
        }
        public void AddMessage(int qNumber, string msg)
        {
            if (!_queues[qNumber].CanWrite)
            {
                throw new Exception("Can't write to queue");
            }
            _queues[qNumber].Send(msg, MessageQueueTransactionType.None);
        }
        public void AddQueue(string name)
        {
            var queue = MessageQueue.Create(@".\Private$\" + name);
            var list = new AccessControlList();
            var entry = new AccessControlEntry(
                new Trustee("Wszyscy"), GenericAccessRights.All,
                StandardAccessRights.All,
                AccessControlEntryType.Allow);
            list.Add(entry);
            queue.SetPermissions(list);
            _queues.Add(queue);
        }

        public void CopyMessages(int from, int to)
        {
            var msgs = _queues[from].GetAllMessages();
            foreach (var m in msgs)
            {
                object body = null;
                try
                {
                    body = m.Body;
                }
                catch
                {
                    body = new object(); // when body is null or corrupted
                }
                _queues[to].Send(body, MessageQueueTransactionType.None);
            }
        }

        public void MoveMessages(int from, int to)
        {
            var msgs = _queues[from].GetAllMessages();
            foreach (var m in msgs)
            {
                object body = null;
                try
                {
                    body = m.Body;
                }
                catch
                {
                    body = new object(); // when body is null or corrupted
                }
                _queues[to].Send(body, MessageQueueTransactionType.None);
            }
            _queues[from].Purge();
        }

        public string ReadTopMessage(int qNumber)
        {
            if(!_queues[qNumber].CanRead)
            {
                throw new Exception("Can't read from queue");
            }
            if(!_queues[qNumber].GetMessageEnumerator2().MoveNext())
            {
                throw new Exception("Queue is empty");
            }
            var msg = _queues[qNumber].Peek();
            msg.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });
            return msg.Body.ToString();
        }

        public List<string> GetQueueNamesWithCount()
        {
            var result = new List<string>();
            for (var i = 0; i < QueuesCount; i++) 
            {
                var e = _queues[i].GetMessageEnumerator2();
                var count = 0;
                try
                {
                    while (e.MoveNext())
                    {
                        count++;
                        if (count >= 10_000)
                            break;
                    }
                }
                catch
                {

                }
                if(count < 10_000)
                    result.Add(_queues[i].QueueName.PadRight(50, '.') + count);
                else
                    result.Add(_queues[i].QueueName.PadRight(50, '.') + count + "+");
            }
            return result;
        }
        public void DeleteQueue(int qNumber)
        {
            if (_queues[qNumber].GetMessageEnumerator2().MoveNext())
            {
                throw new Exception("Clean all messages first");
            }
            MessageQueue.Delete(@".\" + _queues[qNumber].QueueName);
            _queues = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName).ToList();
        }

        public void DeleteTopMessage(int qNumber)
        {
            if (!_queues[qNumber].GetMessageEnumerator2().MoveNext())
            {
                throw new Exception("Queue is empty");
            }
            _queues[qNumber].Receive();
        }

        public void DeleteMessages(int qNumber)
        {
            _queues[qNumber].Purge();
        }
    }
}
