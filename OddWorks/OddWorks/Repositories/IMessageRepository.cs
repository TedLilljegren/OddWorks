using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OddWorks.Models;

namespace OddWorks.Repositories
{
    public interface IMessageRepository
    {
        bool InsertMessage(Message message);
        List<Message> GetMessages();
    }
}
